using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Scoring;

namespace StaplePuck.Core.Fantasy
{
    public class FantasyTeam
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User GM { get; set; }
        public string Name { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public bool IsPaid { get; set; }
        public bool IsValid { get; set; }

        public List<FantasyTeamPlayers> FantasyTeamPlayers { get; set; }

        public int Rank { get; set; }
        public int Score { get; set; }
        public int TodaysScore { get; set; }
    }
}
