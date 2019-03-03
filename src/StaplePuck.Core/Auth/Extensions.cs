using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StaplePuck.Core.Auth
{
    public static class Extensions
    {
        /// <summary>
        /// Adds the auth0 API client.
        /// </summary>
        /// <param name="serviceCollection">The service collection</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddAuth0Client(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.Configure<Auth0Settings>(configuration.GetSection("Auth0"))
                .AddSingleton<IAuth0Client, Auth0Client>();
        }

        /// <summary>
        /// Adds the auth0 authorization client.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The updated service collection.</returns>
        /// <returns></returns>
        public static IServiceCollection AddAuthorizationClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.Configure<AuthorizationSettings>(configuration.GetSection("Authorization"))
                .AddSingleton<IAuthorizationClient, AuthorizationClient>();
        }
    }
}
