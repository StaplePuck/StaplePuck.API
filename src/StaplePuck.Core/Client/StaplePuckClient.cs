using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.Extensions.Options;
using StaplePuck.Core.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using StaplePuck.Core.Auth;

namespace StaplePuck.Core.Client
{
    /// <summary>
    /// Interacts with the StaplePuck API.
    /// </summary>
    public class StaplePuckClient : IStaplePuckClient
    {
        private readonly StaplePuckSettings _settings;
        private readonly GraphQLClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">The settings for StaplePuck.</param>
        public StaplePuckClient(IOptions<StaplePuckSettings> options, IAuth0Client auth)
        {
            _settings = options.Value;
            _client = new GraphQLClient(_settings.Endpoint);

            var token = auth.GetAuthToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Updates an object.
        /// </summary>
        /// <typeparam name="T">The type of object to update.</typeparam>
        /// <param name="mutationName">The name of the mutation.</param>
        /// <param name="value">The value to update.</param>
        /// <returns>The resulting message.</returns>
        public async Task<ResultModel> UpdateAsync<T>(string mutationName, T value, string variableName = null)
        {
            var name = typeof(T).Name;
            var lowerName = Char.ToLowerInvariant(name[0]) + name.Substring(1);

            if (string.IsNullOrEmpty(variableName))
            {
                variableName = lowerName;
            }
            var variables = new ExpandoObject() as IDictionary<string, object>;
            variables.Add(name, value);
            var request = new GraphQLRequest
            {
                Query = string.Concat("mutation ($", lowerName, ": ", name, "Input!){ \n",
                    mutationName, "(", variableName, ": $", lowerName, ") { \n",
                    "    id\n",
                    "    success\n",
                    "    message\n",
                    "  }\n",
                    "}"),
                Variables = variables
            };
            
            var response = await _client.PostAsync(request);
            var data = response.Data as Newtonsoft.Json.Linq.JObject;
            return data.First.First.ToObject<ResultModel>();
        }

        public async Task<T[]> GetAsync<T>(string query, IDictionary<string, object> variables = null)
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

            var response = await _client.PostAsync(request);
            var data = response.Data as Newtonsoft.Json.Linq.JObject;
            var item = data.First.First;
            return item.ToObject<T[]>();
        }
    }
}
