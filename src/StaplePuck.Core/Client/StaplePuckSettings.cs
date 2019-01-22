namespace StaplePuck.Core.Client
{
    /// <summary>
    /// Represents the settings for the StaplePuck API.
    /// </summary>
    public class StaplePuckSettings
    {
        /// <summary>
        /// Gets the HTTP endpoint for the StaplePuck API.
        /// </summary>
        public string Endpoint { get; set; }
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string Audience { get; set; }
        public string ClientSecret { get; set; }
    }
}
