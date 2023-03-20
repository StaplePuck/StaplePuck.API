using Amazon.Lambda;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using NLog.Web;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        var logLevel = Environment.GetEnvironmentVariable("MinLogLevel");

        IdentityModelEventSource.ShowPII = true;
        builder
            .ConfigureLogging((logging) =>
            {
                logging.ClearProviders();
                if (!string.IsNullOrEmpty(logLevel))
                {
                    if (Enum.TryParse(logLevel, out Microsoft.Extensions.Logging.LogLevel logEnum))
                    {
                        logging.SetMinimumLevel(logEnum);
                    }
                }
            })
            .UseNLog()
            .UseStartup<Startup>()
            .UseLambdaServer();
    }

    protected override void Init(IHostBuilder builder)
    {
    }

    public async override Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
    {
        if (request.Resource == "WarmingLambda")
        {
            var concurrencyCount = 1;
            int.TryParse(request.Body, out concurrencyCount);

            if (concurrencyCount > 1)
            {
                Console.WriteLine($"Warming instance {concurrencyCount}.");
                var client = new AmazonLambdaClient();
                await client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest
                {
                    FunctionName = lambdaContext.FunctionName,
                    InvocationType = InvocationType.RequestResponse,
                    Payload = JsonConvert.SerializeObject(new APIGatewayProxyRequest
                    {
                        Resource = request.Resource,
                        Body = (concurrencyCount - 1).ToString()
                    })
                });
            }

            return new APIGatewayProxyResponse { };
        }
        return await base.FunctionHandlerAsync(request, lambdaContext);
    }
}
