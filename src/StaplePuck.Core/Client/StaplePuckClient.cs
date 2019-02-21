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
        public StaplePuckClient(IOptions<StaplePuckSettings> options)
        {
            _settings = options.Value;
            _client = new GraphQLClient(_settings.Endpoint);
            Authenticate();
        }

        private void Authenticate()
        {
            var client = new RestClient(_settings.TokenUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            var json = JsonConvert.SerializeObject(new Auth0Request(_settings));
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var authResponse = JsonConvert.DeserializeObject<Auth0Response>(response.Content);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.access_token);
        }

        /// <summary>
        /// Updates an object.
        /// </summary>
        /// <typeparam name="T">The type of object to update.</typeparam>
        /// <param name="mutationName">The name of the mutation.</param>
        /// <param name="value">The value to update.</param>
        /// <returns>The resulting message.</returns>
        public async Task<ResultModel> UpdateAsync<T>(string mutationName, T value)
        {
            var name = typeof(T).Name;
            var lowerName = Char.ToLowerInvariant(name[0]) + name.Substring(1);

            var variables = new ExpandoObject() as IDictionary<string, object>;
            variables.Add(name, value);
            var request = new GraphQLRequest
            {
                Query = string.Concat("mutation ($", lowerName, ": ", name, "Input!){ \n",
                    mutationName, "(", lowerName, ": $", lowerName, ") { \n",
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

        public async Task<T> GetAsync<T>(string mutationName)
        {
            var name = typeof(T).Name;
            var lowerName = Char.ToLowerInvariant(name[0]) + name.Substring(1);

            //var variables = new ExpandoObject() as IDictionary<string, object>;
            //variables.Add(name, value);
            var request = new GraphQLRequest
            {
                Query = string.Concat("query myQuery { \n",
                    "currentUser { \n",
                    "    id\n",
                    "    name\n",
                    "    email\n",
                    "  }\n",
                    "}")
                //Variables = variables
            };

            var response = await _client.PostAsync(request);
            var data = response.Data as Newtonsoft.Json.Linq.JObject;
            return data.First.First.ToObject<T>();
        }
    }
}
