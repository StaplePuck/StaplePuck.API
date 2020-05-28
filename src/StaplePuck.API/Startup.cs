using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaplePuck.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;
using StaplePuck.Core.Data;
using StaplePuck.Data.Repositories;
using GraphQL.EntityFramework;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Utilities;

using GraphQL.Authorization;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Validation;
using StaplePuck.API.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using FluffySpoon.AspNet.LetsEncrypt;
using FluffySpoon.AspNet.LetsEncrypt.Certes;
using Certes;

namespace StaplePuck.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            GraphTypeTypeRegistry.Register<CalculatedScoreItem, Graphs.CalculatedScoreItemGraph>();
            GraphTypeTypeRegistry.Register<FantasyTeam, Graphs.FantasyTeamGraph>();
            GraphTypeTypeRegistry.Register<FantasyTeamPlayers, Graphs.FantasyTeamPlayersGraph>();
            GraphTypeTypeRegistry.Register<GameDate, Graphs.GameDateGraph>();
            GraphTypeTypeRegistry.Register<GameDateSeason, Graphs.GameDateSeasonGraph>();
            GraphTypeTypeRegistry.Register<League, Graphs.LeagueGraph>();
            GraphTypeTypeRegistry.Register<LeagueMail, Graphs.LeagueMailGraph>();
            GraphTypeTypeRegistry.Register<NumberPerPosition, Graphs.NumberPerPositionGraph>();
            GraphTypeTypeRegistry.Register<PlayerCalculatedScore, Graphs.PlayerCalculatedScoreGraph>();
            GraphTypeTypeRegistry.Register<Player, Graphs.PlayerGraph>();
            GraphTypeTypeRegistry.Register<PlayerCalculatedScore, Graphs.PlayerCalculatedScoreGraph>();
            GraphTypeTypeRegistry.Register<PlayerScore, Graphs.PlayerScoreGraph>();
            GraphTypeTypeRegistry.Register<PlayerSeason, Graphs.PlayerSeasonGraph>();
            GraphTypeTypeRegistry.Register<PlayerStatsOnDate, Graphs.PlayerStatsOnDateGraph>();
            GraphTypeTypeRegistry.Register<PositionType, Graphs.PositionTypeGraph>();
            GraphTypeTypeRegistry.Register<ResultModel, Graphs.ResultGraph>();
            GraphTypeTypeRegistry.Register<ScoringPositions, Graphs.ScoringPositionsGraph>();
            GraphTypeTypeRegistry.Register<ScoringRulePoints, Graphs.ScoringRulePointsGraph>();
            GraphTypeTypeRegistry.Register<ScoringType, Graphs.ScoringTypeGraph>();
            GraphTypeTypeRegistry.Register<Season, Graphs.SeasonGraph>();
            GraphTypeTypeRegistry.Register<Sport, Graphs.SportGraph>();
            GraphTypeTypeRegistry.Register<Team, Graphs.TeamGraph>();
            GraphTypeTypeRegistry.Register<TeamSeason, Graphs.TeamSeasonGraph>();
            GraphTypeTypeRegistry.Register<TeamStateForSeason, Graphs.TeamStateForSeasonGraph>();
            GraphTypeTypeRegistry.Register<User, Graphs.UserGraph>();


            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<StaplePuckContext>(options => options.UseNpgsql(connectionString));
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionString);
            var context = new StaplePuckContext(optionsBuilder.Options);
            services.AddTransient(_ => DbContextBuilder.BuildDBContext(connectionString));
            //services.AddScoped(_ => context);
            services.AddSingleton<Func<StaplePuckContext>>(provider => provider.GetRequiredService<StaplePuckContext>);



            EfGraphQLConventions.RegisterInContainer<StaplePuckContext>(
                services,
                model: StaplePuckContext.GetModel());
            EfGraphQLConventions.RegisterConnectionTypesInContainer(services);

            foreach (var type in GetGraphQlTypes())
            {
                services.AddSingleton(type);
            }

            services.Configure<Auth0APISettings>(Configuration.GetSection("Auth0API"));
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddAuth0Client(Configuration)
                .AddAuthorizationClient(Configuration);
            services.AddScoped<IStatsRepository, StatsRepository>();
            services.AddScoped<IFantasyRepository, FantasyRepository>();
            services.AddScoped<IDocumentExecuter, EfDocumentExecuter>();
            services.AddScoped<ISchema, Models.Schema>();
            services.AddScoped<Models.Mutation>();
            services.AddScoped<IDependencyResolver>(
                provider => new FuncDependencyResolver(provider.GetRequiredService));


            //var mvc = services.AddMvc(option => option.EnableEndpointRouting = false);
            //mvc.SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddCustomCors()
                //.AddAuthorization()
                .AddCustomGraphQL(this.HostingEnvironment)
                .AddCustomGraphQLAuthorization(Configuration);
#if !DEBUG
            //ConfigureSSL(services, Configuration);
#endif

            var mvc = services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            mvc.AddNewtonsoftJson();

            ConfigureAuth(services);
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0API:Domain"]}/";
                options.Audience = Configuration["Auth0API:Audience"];
            });
        }

        private void ConfigureSSL(IServiceCollection services, IConfiguration configuration)
        {
            var settings = new LetsEncryptSettings();
            configuration.GetSection("LetsEncrypt").Bind(settings);
            if (settings != null && !string.IsNullOrEmpty(settings.Email))
            {
                var isDebug = false;
#if DEBUG
                isDebug = true;
#endif
                services.AddFluffySpoonLetsEncrypt(new LetsEncryptOptions
                {
                    Email = settings.Email,
                    UseStaging = isDebug,
                    Domains = settings.Domains.Split(","),
                    TimeUntilExpiryBeforeRenewal = TimeSpan.FromDays(30),
                    TimeAfterIssueDateBeforeRenewal = TimeSpan.FromDays(7),
                    CertificateSigningRequest = new CsrInfo()
                    {
                        CountryName = settings.CountryName,
                        Locality = settings.Locality,
                        Organization = settings.Organization,
                        OrganizationUnit = settings.OrganizationUnit,
                        State = settings.State
                    }
                });
                services.AddFluffySpoonLetsEncryptFileCertificatePersistence("LetsEncrypt/Certificate");
                services.AddFluffySpoonLetsEncryptFileChallengePersistence("LetsEncrypt/Challenge");
            }
        }

        static IEnumerable<Type> GetGraphQlTypes()
        {
            return typeof(Startup).Assembly
                .GetTypes()
                .Where(x => !x.IsAbstract &&
                            (typeof(IObjectGraphType).IsAssignableFrom(x) ||
                             typeof(IInputObjectGraphType).IsAssignableFrom(x)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StaplePuckContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.EnsureSeedData();
            app.UseCors(CorsPolicyName.AllowAny)
               .UseAuthentication()
               .UseGraphQLWebSockets<ISchema>()
               .UseGraphQL<ISchema>("/graphql")
               .UseGraphiQLServer(new GraphiQLOptions())
               .UseMvc();
#if !DEBUG
            app.UseFluffySpoonLetsEncrypt();
#endif
        }
    }
}
