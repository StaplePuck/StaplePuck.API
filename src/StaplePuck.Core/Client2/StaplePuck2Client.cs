using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using StaplePuck.Core.Models;
using System.Linq;

namespace StaplePuck.Core.Client2
{
    public class StaplePuck2Client : IStaplePuck2Client
    {
        private readonly GraphQLHttpClient _client;
        private readonly ILogger _logger;
        private readonly JsonSerializerOptions _serializerOptions;

        public StaplePuck2Client(IOptions<StaplePuck2Settings> options, ILogger<StaplePuck2Client> logger)
        {
            var settings = options.Value;
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _client = new GraphQLHttpClient(settings.Url, new SystemTextJsonSerializer(_serializerOptions));

            _client.HttpClient.DefaultRequestHeaders.Add("x-api-key", settings.Token);
            _logger = logger;
        }

        public async Task<T?> ExecuteMutationAsync<T>(GraphQLRequest request, string mutationName, CancellationToken cancellationToken) where T : class
        {
            try
            {
                var temp = _client.JsonSerializer.SerializeToString(request);
                var result = await _client.SendMutationAsync<dynamic>(request, cancellationToken);
                if (result == null)
                {
                    throw new StaplePuck2Exception("Failed to get response from mutation");
                }
                if (result.Errors != null && result.Errors.Length > 0)
                {
                    throw new StaplePuck2Exception(string.Join(", ", result?.Errors.Select(x => x.Message)));
                }

                string stringResult = result.Data.ToString();
                if (stringResult == null)
                {
                    throw new Exception("Unable to get data response");
                }
                stringResult = stringResult.Replace($"\"{mutationName}\":", string.Empty);
                stringResult = stringResult.Remove(0, 1);
                stringResult = stringResult.Remove(stringResult.Length - 1, 1);

                var data = JsonSerializer.Deserialize<T>(stringResult, _serializerOptions);
                return data;
            }
            catch (Exception ex)
            {
                throw new StaplePuck2Exception("Exception occured during call to mutation", ex);
            }
        }

        public async Task<T> ExecuteQuery<T>(GraphQLRequest request, string queryName, CancellationToken cancellationToken)
        {
            try
            {
                var temp = _client.JsonSerializer.SerializeToString(request);
                var result = await _client.SendQueryAsync<dynamic>(request, cancellationToken);
                if (result == null)
                {
                    throw new StaplePuck2Exception("Failed to get response from mutation");
                }
                if (result.Errors != null && result.Errors.Length > 0)
                {
                    throw new StaplePuck2Exception(string.Join(", ", result?.Errors.Select(x => x.Message)));
                }

                var stringResult = result.Data.ToString();
                if (stringResult == null)
                {
                    throw new Exception("Unable to get data response");
                }
                stringResult = stringResult.Replace($"\"{queryName}\":", string.Empty);
                stringResult = stringResult.Remove(0, 1);
                stringResult = stringResult.Remove(stringResult.Length - 1, 1);

                var data = JsonSerializer.Deserialize<T>(stringResult, _serializerOptions);
                return data;
            }
            catch (Exception ex)
            {
                throw new StaplePuck2Exception("Exception occured during call to mutation", ex);
            }
        }
    }
}
