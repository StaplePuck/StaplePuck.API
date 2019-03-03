using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Core.Auth
{
    /// <summary>
    /// Provides an interface for interacting with the Auth0 authorization apis.
    /// </summary>
    public interface IAuthorizationClient
    {
        /// <summary>
        /// Creates a group.
        /// </summary>
        /// <param name="groupName">The name of the group.</param>
        /// <returns>The id of the created group.</returns>
        Task<string> CreateGroupAsync(string groupName);

        /// <summary>
        /// Adds a user to the group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <param name="sub">The id of the user.</param>
        /// <returns>The task completion source.</returns>
        Task AddUserToGroup(string groupId, string sub);
    }
}
