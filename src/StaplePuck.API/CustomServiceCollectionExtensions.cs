using GraphQL;
using GraphQL.Authorization;
using GraphQL.Server;
using GraphQL.Server.Internal;
using GraphQL.Validation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StaplePuck.API.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaplePuck.API
{
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services, IHostingEnvironment hostingEnvironment) =>
            services
                // Add a way for GraphQL.NET to resolve types.
                .AddSingleton<IDependencyResolver, GraphQLDependencyResolver>()
                .AddGraphQL(
                    options =>
                    {
                        // Set some limits for security, read from configuration.
                        options.ComplexityConfiguration = services
                            .BuildServiceProvider()
                            .GetRequiredService<IOptions<GraphQLOptions>>()
                            .Value
                            .ComplexityConfiguration;
                        // Show stack traces in exceptions. Don't turn this on in production.
                        options.ExposeExceptions = hostingEnvironment.IsDevelopment();
                    })
                // Adds all graph types in the current assembly with a singleton lifetime.
                .AddGraphTypes()
                // Adds ConnectionType<T>, EdgeType<T> and PageInfoType.
                .AddRelayGraphTypes()
                // Add a user context from the HttpContext and make it available in field resolvers.
                .AddUserContextBuilder<GraphQLUserContextBuilder>()
                // Add GraphQL data loader to reduce the number of calls to our repository.
                .AddDataLoader()
                .Services;
                //.AddTransient(typeof(IGraphQLExecuter<>), typeof(InstrumentingGraphQLExecutor<>));

        
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
                            y => y.RequireClaim("role", "admin"));
                        authorizationSettings.AddPolicy(
                            "write:stats", policy => policy.AddRequirement(new Auth.HasScopeRequirement("write:stats", $"https://{configuration["Auth0:Domain"]}/")));
                        return authorizationSettings;
                    });

        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        public static IMvcCoreBuilder AddCustomCors(this IMvcCoreBuilder builder) =>
            builder.AddCors(
                options =>
                {
                    // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
                    // or a [EnableCors("PolicyName")] attribute on your controller or action.
                    options.AddPolicy(
                        CorsPolicyName.AllowAny,
                        x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
    }
}
