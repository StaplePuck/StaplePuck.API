using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Core.Auth
{
    /// <summary>
    /// Provides a class to interact with the Auth0 authorization APIs.
    /// </summary>
    public class AuthorizationClient : IAuthorizationClient
    {
        private readonly AuthorizationSettings _settings;
        private readonly IRestClient _restClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">The configuration options.</param>
        /// <param name="auth0Client">The auth0 client information.</param>
        public AuthorizationClient(IOptions<AuthorizationSettings> options, IAuth0Client auth0Client)
        {
            _settings = options.Value;
            _restClient = new RestClient(_settings.BaseUrl);

            var token = auth0Client.GetAuthToken();
            _restClient.Authenticator = new JwtAuthenticator(token);
        }

        /// <summary>
        /// Creates a group.
        /// </summary>
        /// <param name="groupName">The name of the group.</param>
        /// <returns>The id of the created group.</returns>
        public async Task<string> CreateGroupAsync(string groupName)
        {
            var request = new RestRequest("groups", Method.POST);
            request.AddParameter("name", groupName);
            request.AddParameter("description", $"Security group for {groupName}");

            var response = await _restClient.PostAsync<GroupResponse>(request);
            return response._id;
        }

        /// <summary>
        /// Adds a user to the group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <param name="sub">The id of the user.</param>
        /// <returns>The task completion source.</returns>
        public async Task AddUserToGroup(string groupId, string sub)
        {
            var request = new RestRequest($"groups/{groupId}/members", Method.PATCH);
            var list = new List<string>();
            list.Add(sub);
            request.AddJsonBody(list);

            var response = _restClient.Execute(request);
            await Task.CompletedTask;
        }

        private class GroupResponse
        {
            public string name { get; set; }
            public string _id { get; set; }
        }
    }
}

