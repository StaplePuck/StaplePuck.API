using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using StaplePuck.Core.Stats;
using System.Threading.Tasks;

namespace StaplePuck.Data.Repositories
{
    public interface IStatsRepository
    {
        Task<ResultModel> Add(StaplePuckContext context, Season season);

        Task<ResultModel> Update(StaplePuckContext context, GameDate gameDate);

        Task<ResultModel> Update(StaplePuckContext context, League league);

        Task<ResultModel> Update(StaplePuckContext context, PlayerStatsOnDate playerStatsOnDate);
        Task<ResultModel> Update(StaplePuckContext context, TeamStateForSeason[] teamStates);
    }
}
