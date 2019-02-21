﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaplePuck.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StaplePuck.Core.Data;
using StaplePuck.Data.Repositories;
using GraphQL.EntityFramework;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Types;

using GraphQL.Authorization;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Validation;

//using GraphiQl;

namespace StaplePuck.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<StaplePuckContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Singleton);

            var optionsBuilder = new DbContextOptionsBuilder<StaplePuckContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var myDataContext = new StaplePuckContext(optionsBuilder.Options))
            {
                EfGraphQLConventions.RegisterInContainer(services, myDataContext);
            }

            foreach (var type in GetGraphQlTypes())
            {
                services.AddSingleton(type);
            }

            services.Configure<Auth.Auth0Settings>(Configuration.GetSection("Auth0"));
            services.AddSingleton<IStatsRepository, StatsRepository>();
            services.AddSingleton<IFantasyRepository, FantasyRepository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ISchema, Models.Schema>();
            services.AddSingleton<Models.Mutation>();
            services.AddSingleton<IDependencyResolver>(
                provider => new FuncDependencyResolver(provider.GetRequiredService));
            
            services.AddScoped<IAuthorizationHandler,
                          Auth.TeamAuthorizationHandler>();

            var mvc = services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddAuthorization()
                .AddJsonFormatters()
                .Services
                .AddCustomGraphQL(this.HostingEnvironment)
                .AddCustomGraphQLAuthorization(Configuration)
                .BuildServiceProvider();
            
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
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StaplePuckContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            db.EnsureSeedData();
            app.UseGraphQL<ISchema>("/graphql");
            app.UseGraphiQLServer(new GraphiQLOptions());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
