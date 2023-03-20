using GraphiQl;
using GraphQL;
using GraphQL.Types;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Data;
using StaplePuck.Data;
using StaplePuck.Data.Repositories;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        Configuration = configuration;
        HostingEnvironment = hostingEnvironment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment HostingEnvironment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        EfGraphQLConventions.RegisterInContainer<StaplePuckContext>(
            services,
            model: StaplePuckContext.StaticModel);

        foreach (var type in GetGraphQlTypes())
        {
            services.AddSingleton(type);
        }

        var connectionString = Configuration["ConnectionStrings:Default"];
        if (connectionString == null)
        {
            throw new Exception("Connection string not defined");
        }
        var dbContextBuilder = new DbContextBuilder(connectionString);
        services.AddSingleton<IHostedService>(dbContextBuilder);
        services.AddSingleton<Func<StaplePuckContext>>(_ => dbContextBuilder.BuildDbContext);
        services.AddSingleton(_ => dbContextBuilder.BuildDbContext());
        services.AddSingleton<IDocumentExecuter, EfDocumentExecuter>();
        services.AddSingleton<ISchema, Schema>();

        services.Configure<Auth0APISettings>(Configuration.GetSection("Auth0API"));
        services.Configure<SNSSettings>(Configuration.GetSection("AWS"));
        services.AddAuth0Client(Configuration)
                .AddAuthorizationClient(Configuration);
        services.AddSingleton<IMessageEmitter, MessageEmitter>();
        services.AddSingleton<IStatsRepository, StatsRepository>();
        services.AddSingleton<IFantasyRepository, FantasyRepository>();
        
        services.AddCustomGraphQLAuthorization(Configuration);
        services.AddAuthorization()
            .AddAuthentication();
        
        services.AddGraphQL(b => b
            .AddAutoSchema<Query>(s => s
            .WithMutation<Mutation>())
            .AddUserContextBuilder(httpContext => new GraphQLUserContext(httpContext.User))
            .AddSystemTextJson());
        services.AddRouting();
        services.AddCustomCors();
        services.ConfigureAuth(Configuration);
    }

    static IEnumerable<Type> GetGraphQlTypes() =>
        typeof(Startup).Assembly
            .GetTypes()
            .Where(_ => !_.IsAbstract &&
                        (typeof(IObjectGraphType).IsAssignableFrom(_) ||
                         typeof(IInputObjectGraphType).IsAssignableFrom(_)));

    public void Configure(IApplicationBuilder builder)
    {
        builder.UseRouting();
        builder.UseCors(CorsPolicyName.AllowAny)
            .UseAuthentication()
            .UseAuthorization();
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL("/graphql")
                .RequireCors(CorsPolicyName.AllowAny);
        });

        //builder.UseWebSockets();
        //builder.UseGraphQLWebSockets<ISchema>();
        builder.UseGraphiQl("/graphiql", "/graphql");
    }
}
