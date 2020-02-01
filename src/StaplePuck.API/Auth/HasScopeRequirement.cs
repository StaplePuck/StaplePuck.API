using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Authorization;

namespace StaplePuck.API.Auth
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }

        public Task Authorize(AuthorizationContext context)
        {
            var claims = context.User.Claims.ToList();

            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == this.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == this.Issuer).Value.Split(' ');

            // Succeed if the scope array contains the required scope
            if (!scopes.Any(s => s == this.Scope))
                context.ReportError($"Missing scope of {this.Scope}");
            
            return Task.CompletedTask;
        }
    }
}
