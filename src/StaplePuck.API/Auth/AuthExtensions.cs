using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace StaplePuck.API.Auth
{
    public static class AuthExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal, Auth0Settings settings)
        {
            var claims = claimsPrincipal.Claims.ToList();

            // If user does not have the scope claim, get out of here
            if (!claimsPrincipal.HasClaim(c => c.Type == "sub" && c.Issuer == settings.Authority))
            {
                return null;
            }
            var sub = claimsPrincipal.FindFirst(c => c.Type == "sub" && c.Issuer == settings.Authority).Value;
            return sub;
        }
    }
}
