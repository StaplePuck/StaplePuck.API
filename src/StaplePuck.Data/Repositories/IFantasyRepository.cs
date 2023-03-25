using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaplePuck.Data.Repositories
{
    public interface IFantasyRepository
    {
        Task<ResultModel> Update(StaplePuckContext context, User user);
        Task<ResultModel> Add(StaplePuckContext context, League league);
        Task<ResultModel> Update(StaplePuckContext context, League league);
        Task<ResultModel> Add(StaplePuckContext context, FantasyTeam team, string userExternalId);
        Task<bool> UsernameAlreadyExists(StaplePuckContext context, string username, string userExternalId);
        Task<bool> EmailAlreadyExists(StaplePuckContext context, string email, string userExternalId);
        Task<ResultModel> Update(StaplePuckContext context, FantasyTeam team, bool isValid);
        Task<List<string>> Validate(StaplePuckContext context, FantasyTeam team);
        Task<bool> ValidateUserIsAssignedGM(StaplePuckContext context, int teamId, string userExternalId);
        Task<List<string>> ValidateNew(StaplePuckContext context, FantasyTeam team, string userExternalId);
        Task<ResultModel> Add(StaplePuckContext context, NotificationToken notificationToken, string userExternalId);
        Task<ResultModel> Remove(StaplePuckContext context, NotificationToken notificationToken);
    }
}
