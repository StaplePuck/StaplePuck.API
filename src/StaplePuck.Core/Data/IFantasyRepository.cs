using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Data
{
    public interface IFantasyRepository
    {
        Task<ResultModel> Update(User user);
        Task<ResultModel> Add(League league);
        Task<ResultModel> Update(League league);
        Task<ResultModel> Add(FantasyTeam team, string userExternalId);
        Task<ResultModel> Update(FantasyTeam team);
    }
}
