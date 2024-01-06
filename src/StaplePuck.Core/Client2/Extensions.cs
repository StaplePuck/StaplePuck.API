using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StaplePuck.Core.Client2
{
    /// <summary>
    /// Provides extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the StaplePuck API client.
        /// </summary>
        /// <param name="serviceCollection">The service collection</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddStaplePuck2Client(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.Configure<StaplePuck2Settings>(configuration.GetSection("StaplePuck2"))
                .AddSingleton<IStaplePuck2Client, StaplePuck2Client>();
        }
    }
}
