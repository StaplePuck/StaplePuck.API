using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace StaplePuck.Core.Auth
{
    /// <summary>
    /// Provides a class to interact with the Auth0 authorization APIs.
    /// </summary>
    public class AuthorizationClient : IAuthorizationClient
    {
        private readonly AuthorizationSettings _settings;
        private readonly IRestClient _restClient;
        private readonly string _issuer;
        private readonly string _authority;
        private readonly IAuth0Client _auth0Client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">The configuration options.</param>
        /// <param name="auth0Client">The auth0 client information.</param>
        public AuthorizationClient(IOptions<AuthorizationSettings> options, IOptions<Auth0APISettings> auth0Options, IAuth0Client auth0Client)
        {
            _settings = options.Value;
            _issuer = auth0Options.Value.Domain;
            _authority = auth0Options.Value.Authority;
            _restClient = new RestClient(_settings.BaseUrl);
            _auth0Client = auth0Client;

            var token = auth0Client.GetAuthToken();
            if (token != null)
            {
                _restClient.Authenticator = new JwtAuthenticator(token);
            }
        }

        /// <summary>
        /// Creates a group.
        /// </summary>
        /// <param name="groupName">The name of the group.</param>
        /// <returns>The id of the created group.</returns>
        private Task<string> CreateGroupAsync(string groupName)
        {
            if (_restClient.Authenticator == null)
            {
                return Task.FromResult(string.Empty);
            }
            var request = new RestRequest("groups", Method.POST);
            request.AddParameter("name", groupName);
            request.AddParameter("description", $"Security group for {groupName}");

            var response = _restClient.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var token = _auth0Client.GetAuthToken();
                _restClient.Authenticator = new JwtAuthenticator(token);
                response = _restClient.Post(request);
                //if (response.IsSuccessful)
            }
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GroupResponse>(response.Content);
            return Task.FromResult(result._id);
        }

        /// <summary>
        /// Adds a user to the group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <param name="sub">The id of the user.</param>
        /// <returns>The task completion source.</returns>
        private Task AddUserToGroup(string groupId, string sub)
        {
            if (_restClient.Authenticator == null)
            {
                return Task.FromResult(string.Empty);
            }
            var request = new RestRequest($"groups/{groupId}/members", Method.PATCH);
            var list = new List<string>();
            list.Add(sub);
            request.AddJsonBody(list);

            var response = _restClient.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var token = _auth0Client.GetAuthToken();
                _restClient.Authenticator = new JwtAuthenticator(token);
                response = _restClient.Execute(request);
                // todo log error message if not success
            }
            return Task.CompletedTask;
        }

        public async Task AssignUserAsGM(string subjectId, int teamId)
        {
            var groupId = await CreateGroupAsync(this.GetGMGroupId(teamId));
            await this.AddUserToGroup(groupId, subjectId);
        }

        public async Task AssignUserAsCommissioner(string subjectId, int leagueId)
        {
            var groupId = await CreateGroupAsync(this.GetCommisionerGroupId(leagueId));
            await this.AddUserToGroup(groupId, subjectId);
        }

        public bool UserIsGM(ClaimsPrincipal user, int teamId)
        {
            var groupId = this.GetGMGroupId(teamId);
            return this.HasScope(user, groupId);
        }

        public bool UserIsCommissioner(ClaimsPrincipal user, int leagueId)
        {
            var groupId = this.GetCommisionerGroupId(leagueId);
            return this.HasScope(user, groupId);
        }

        public bool UserIsAdmin(ClaimsPrincipal user)
        {
            return this.HasScope(user, "Admin");
        }

        private string GetCommisionerGroupId(int leagueId)
        {
            return $"League:{_settings.SiteName}:{leagueId}";
        }

        private string GetGMGroupId(int teamId)
        {
            return $"Team:{_settings.SiteName}:{teamId}";
        }

        private bool HasScope(ClaimsPrincipal principal, string groupName)
        {
            if (!principal.HasClaim(c => c.Type == "scope" && (c.Issuer == this._issuer || c.Issuer == this._authority)))
                return false;

            // Split the scopes string into an array
            var scopes = principal.FindFirst(c => c.Type == "scope" && (c.Issuer == this._issuer || c.Issuer == this._authority)).Value.Split(' ');

            // Succeed if the scope array contains the required scope
            return scopes.Any(s => s == groupName);
        }

        private class GroupResponse
        {
            public string name { get; set; }
            public string _id { get; set; }
        }
    }
}

