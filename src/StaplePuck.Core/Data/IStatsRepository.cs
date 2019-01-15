using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Data
{
    public interface IStatsRepository
    {
        Task<ResultModel> Add(Season season);
    }
}
