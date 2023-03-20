using Microsoft.AspNetCore.Authentication.JwtBearer;
using StaplePuck.Core.Auth;
using System.Security.Claims;

public static class AuthExtensions
{
    public static string? GetUserId(this ClaimsPrincipal claimsPrincipal, Auth0APISettings settings)
    {
        var claims = claimsPrincipal.Claims.ToList();

        // If user does not have the scope claim, get out of here
        if (!claimsPrincipal.HasClaim(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Issuer == settings.Authority))
        {
            return null;
        }
        var sub = claimsPrincipal.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Issuer == settings.Authority)?.Value;
        return sub;
    }

    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.Authority = $"https://{configuration["Auth0API:Domain"]}/";
            options.Audience = configuration["Auth0API:Audience"];
            //options.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration();
        });
        return services;
    }
}
