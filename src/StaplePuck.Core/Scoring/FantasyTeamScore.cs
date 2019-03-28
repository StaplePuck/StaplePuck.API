using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Scoring
{
    public class FantasyTeamScore
    {
        public int Id { get; set; }

        public int FantasyTeamId { get; set; }
        public FantasyTeam FantasyTeam { get; set; }
        
        public DateTime Date { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public int TodaysScore { get; set; }
    }
}
