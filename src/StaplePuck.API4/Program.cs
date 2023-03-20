using Microsoft.Extensions.Logging;
using NLog.Web;

var logLevel = Environment.GetEnvironmentVariable("MinLogLevel");
var webHostBuilder = WebHost.CreateDefaultBuilder();
webHostBuilder.ConfigureLogging((context, logging) =>
{
    logging.ClearProviders();
    if (!string.IsNullOrEmpty(logLevel))
    {
        if (Enum.TryParse(logLevel, out LogLevel logEnum))
        {
            logging.SetMinimumLevel(logEnum);
        }
    }
});
webHostBuilder.UseNLog();
var hostBuilder = webHostBuilder.UseStartup<Startup>();
hostBuilder.Build().Run();