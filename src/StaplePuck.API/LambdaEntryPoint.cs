using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Amazon.Lambda.Core;
using Amazon.Lambda.AspNetCoreServer;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda;
using Newtonsoft.Json;

namespace StaplePuck.API
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        public LambdaEntryPoint()
        {
        }

        protected override void Init(IWebHostBuilder builder)
        {
            IdentityModelEventSource.ShowPII = true;
            builder
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
                    Console.WriteLine($"Warming instance { concurrencyCount}.");
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
}
