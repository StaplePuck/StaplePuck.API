using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace StaplePuck.Core.Auth
{
    /// <summary>
    /// Provides a class for communicating to Auth0.
    /// </summary>
    public class Auth0Client : IAuth0Client
    {
        private readonly Auth0Settings _settings;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings">The auth0 settings.</param>
        public Auth0Client(IOptions<Auth0Settings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// Gets the machine to machine application auth token.
        /// </summary>
        /// <returns>The auth token.</returns>
        public string GetAuthToken()
        {
            var client = new RestClient(_settings.TokenUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            var json = JsonConvert.SerializeObject(new Auth0Request(_settings));
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var authResponse = JsonConvert.DeserializeObject<Auth0Response>(response.Content);
            return authResponse.access_token;
        }
    }
}
