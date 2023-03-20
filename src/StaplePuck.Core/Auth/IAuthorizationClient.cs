using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        /// Assigns a user to the GM group.
        /// </summary>
        /// <param name="subjectId">The subject id of the user.</param>
        /// <param name="teamId">The team id.</param>
        /// <returns>The task completion source.</returns>
        Task AssignUserAsGM(string subjectId, int teamId);

        /// <summary>
        /// Assigns a user to the leqauge group.
        /// </summary>
        /// <param name="subjectId">The subject id of the user.</param>
        /// <param name="leagueId">The league id.</param>
        /// <returns>The task completion source.</returns>
        Task AssignUserAsCommissioner(string subjectId, int leagueId);

        bool UserIsGM(ClaimsPrincipal user, int teamId);

        bool UserIsCommissioner(ClaimsPrincipal user, int leagueId);

        bool UserIsAdmin(ClaimsPrincipal user);
    }
}
