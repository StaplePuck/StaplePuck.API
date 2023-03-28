using GraphQL.Client;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;
using StaplePuck.Core.Auth;
using GraphQL;
using StaplePuck.Core.Models;

namespace StaplePuck.Core.Client
{
    /// <summary>
    /// Interacts with the StaplePuck API.
    /// </summary>
    public class StaplePuckClient : IStaplePuckClient
    {
        private readonly StaplePuckSettings _settings;
        private readonly GraphQLHttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">The settings for StaplePuck.</param>
        public StaplePuckClient(IOptions<StaplePuckSettings> options, IAuth0Client auth)
        {
            _settings = options.Value;
            _client = new GraphQLHttpClient(_settings.Endpoint, new SystemTextJsonSerializer());

            auth.OnNewToken += Auth_OnNewToken;
            var token = auth.GetAuthToken();
            if (token != null)
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private void Auth_OnNewToken(string token)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Updates an object.
        /// </summary>
        /// <typeparam name="T">The type of object to update.</typeparam>
        /// <param name="mutationName">The name of the mutation.</param>
        /// <param name="value">The value to update.</param>
        /// <param name="typeName">The name of the type for the variable.</param>
        /// <returns>The resulting message.</returns>
        public async Task<ResultModel> UpdateAsync<T>(string mutationName, T value, string? variableName = null, string? typeName = null)
        {
            var name = typeof(T).Name;
            var inputName = string.Concat(name, "Input");
            if (!string.IsNullOrEmpty(typeName))
            {
                name = typeName.Trim('[', ']');
                inputName = typeName;
            }
            var lowerName = (char.ToLowerInvariant(name[0]) + name.Substring(1));

            if (string.IsNullOrEmpty(variableName))
            {
                variableName = lowerName;
            }
            
            var variables = new ExpandoObject() as IDictionary<string, object?>;
            variables.Add(lowerName, value);
            var request = new GraphQLRequest
            {
                Query = string.Concat("mutation ($", lowerName, ": ", inputName, "!){ \n",
                    mutationName, "(", variableName, ": $", lowerName, ") { \n",
                    "    id\n",
                    "    success\n",
                    "    message\n",
                    "  }\n",
                    "}"),
                Variables = variables
            };

            try
            {
                var response = await _client.SendMutationAsync<dynamic>(request);
                if (response == null)
                {
                    return new ResultModel { Success = false, Message = "Failed to get response from mutation" };
                }
                if (response.Errors != null && response.Errors.Length > 0)
                {
                    return new ResultModel { Success = false, Message = string.Join(", ", response?.Errors.Select(x => x.Message)) };
                }

                var stringResult = response.Data.ToString();
                if (stringResult == null)
                {
                    throw new Exception("Unable to get data response");
                }
                stringResult = stringResult.Replace($"\"{mutationName}\":", string.Empty);
                stringResult = stringResult.Remove(0, 1);
                stringResult = stringResult.Remove(stringResult.Length - 1, 1);

                var result = JsonSerializer.Deserialize<ResultModel>(stringResult);
                return result;
            }
            catch (Exception e)
            {
                return new ResultModel { Success = false, Message = e.Message };
            }
        }

        /// <summary>
        /// Queries for a collection of objects.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="query">The graphql query.</param>
        /// <param name="variables">The collection of paramaters.</param>
        /// <returns>The query response.</returns>
        public async Task<T[]> GetAsyncCollection<T>(string query, IDictionary<string, object>? variables = null)
        {
            var name = typeof(T).Name;

            //var variables = new ExpandoObject() as IDictionary<string, object>;
            //variables.Add(name, value);
            var request = new GraphQLRequest
            {
                Query = query
                //Variables = variables
            };
            if (variables != null)
            {
                request.Variables = variables;
            }
            
            var response = await _client.SendQueryAsync<T[]>(request);
            return response.Data;
        }

        /// <summary>
        /// Queries for graphs.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="query">The graphql query.</param>
        /// <param name="variables">The collection of paramaters.</param>
        /// <returns>The query response.</returns>
        public async Task<T> GetAsync<T>(string query, IDictionary<string, object>? variables = null)
        {
            var name = typeof(T).Name;

            //var variables = new ExpandoObject() as IDictionary<string, object>;
            //variables.Add(name, value);
            var request = new GraphQLRequest
            {
                Query = query
                //Variables = variables
            };
            if (variables != null)
            {
                request.Variables = variables;
            }

            var response = await _client.SendQueryAsync<T>(request);
            return response.Data;
        }
    }
}
