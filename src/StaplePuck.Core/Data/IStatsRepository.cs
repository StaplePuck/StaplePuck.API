using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Data
{
    public interface IStatsRepository
    {
        Task<ResultModel> Add(Season season);

        Task<ResultModel> Update(GameDate gameDate);

        Task<ResultModel> Update(League league);

        Task<ResultModel> Update(PlayerStatsOnDate playerStatsOnDate);
    }
}
