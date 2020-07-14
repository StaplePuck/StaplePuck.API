using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Data
{
    public interface IFantasyRepository
    {
        Task<ResultModel> Update(User user);
        Task<ResultModel> Add(League league);
        Task<ResultModel> Update(League league);
        Task<ResultModel> Add(FantasyTeam team, string userExternalId);
        Task<bool> UsernameAlreadyExists(string username, string userExternalId);
        Task<bool> EmailAlreadyExists(string email, string userExternalId);
        Task<ResultModel> Update(FantasyTeam team, bool isValid);
        Task<List<string>> Validate(FantasyTeam team);
        Task<List<string>> ValidateNew(FantasyTeam team, string userExternalId);
    }
}
