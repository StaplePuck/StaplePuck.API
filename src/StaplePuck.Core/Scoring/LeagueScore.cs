using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Scoring
{
    public class LeagueScore
    {
        public League League { get; set; }
        public IEnumerable<TeamScore> Teams { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, PlayerScore> Players { get; private set; }
    }
}
