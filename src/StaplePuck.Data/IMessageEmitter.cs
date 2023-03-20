using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core.Models;

namespace StaplePuck.Data
{
    public interface IMessageEmitter
    {
        Task StatsUpdated(StatsUpdated data);
        Task ScoreUpdated(ScoreUpdated scoreUpdated);
    }
}
