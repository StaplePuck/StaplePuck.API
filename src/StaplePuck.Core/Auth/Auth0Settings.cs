namespace StaplePuck.Core.Auth
{
    public class Auth0Settings
    {
        public string TokenUrl { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
