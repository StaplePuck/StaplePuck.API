using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace StaplePuck.Core
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// Adds NLog Logging to the host builder.
        /// </summary>
        /// <param name="builder">The builder to update.</param>
        /// <returns>The udpated builder.</returns>
        public static IHostBuilder AddNLog(this IHostBuilder builder)
        {
            var logLevel = Environment.GetEnvironmentVariable("MinLogLevel");

            return builder.ConfigureLogging((logging) =>
                {
                     logging.ClearProviders();
                     if (!string.IsNullOrEmpty(logLevel))
                     {
                         if (Enum.TryParse(logLevel, out LogLevel logEnum))
                         {
                             logging.SetMinimumLevel(logEnum);
                         }
                     }
                 })
                .UseNLog();
        }
    }
}
