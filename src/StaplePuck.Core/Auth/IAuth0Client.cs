namespace StaplePuck.Core.Auth
{
    /// <summary>
    /// Provides an interface for getting information from Auth0.
    /// </summary>
    public interface IAuth0Client
    {
        /// <summary>
        /// Fires on new token.
        /// </summary>
        event NewToken OnNewToken;
        /// <summary>
        /// Gets the machine to machine application auth token.
        /// </summary>
        /// <returns>The auth token.</returns>
        string? GetAuthToken();
    }
}
