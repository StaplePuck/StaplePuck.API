using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Scoring
{
    public class TeamScore
    {
        public FantasyTeam FantasyTeam { get; set; }
        public IEnumerable<PlayerCalculatedScore> Players { get; set; }

        public DateTime Date { get; set; }
        public int Rank { get; set; }
        public int Score { get { return Players.Sum(x => x.Score); } }
        public int TodaysScore { get { return Players.Sum(x => x.TodaysScore); } }
    }
}
