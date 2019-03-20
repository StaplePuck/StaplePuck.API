using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Scoring
{
    public class LeagueScore
    {
        public int LeagueId { get; set; }
        public League League { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, PlayerCalculatedScore> Players { get; private set; }
    }
}
