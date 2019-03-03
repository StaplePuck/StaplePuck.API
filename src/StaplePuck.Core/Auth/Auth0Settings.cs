namespace StaplePuck.Core.Auth
{
    public class Auth0Settings
    {
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string Audience { get; set; }
        public string ClientSecret { get; set; }
    }
}
