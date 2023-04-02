using GraphQL.Authorization;
using GraphQL.Validation;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add GraphQL authorization (See https://github.com/graphql-dotnet/authorization).
    /// </summary>
    public static IServiceCollection AddCustomGraphQLAuthorization(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>()
            .AddTransient<IValidationRule, AuthorizationValidationRule>()
            .AddSingleton(
                x =>
                {
                    var authorizationSettings = new AuthorizationSettings();

                    authorizationSettings.AddPolicy(
                        AuthorizationPolicyName.Admin,
                        y => y.AddRequirement(new HasScopeRequirement("admin", $"https://{configuration["Auth0API:Domain"]}/")));
                    authorizationSettings.AddPolicy(
                        AuthorizationPolicyName.WriteStats,
                        y => y.AddRequirement(new HasScopeRequirement("write:stats", $"https://{configuration["Auth0API:Domain"]}/")));
                    return authorizationSettings;
                });

    /// <summary>
    /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
    /// https://docs.asp.net/en/latest/security/cors.html
    /// </summary>
    public static IServiceCollection AddCustomCors(this IServiceCollection builder) =>
        builder.AddCors(
            options =>
            {
                // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
                // or a [EnableCors("PolicyName")] attribute on your controller or action.
                options.AddPolicy(
                    CorsPolicyName.AllowAny,
                    x => x
                        .WithOrigins("https://www.staplepuck.com", "https://staplepuck.com", "https://beta.staplepuck.com", "http://localhost:8080", "http://localhost:5000", "https://localhost:5001")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials());
            });
}

