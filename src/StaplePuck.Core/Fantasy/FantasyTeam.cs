using System;
using System.Collections.Generic;
using System.Text;

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

        public Scoring.FantasyTeamScore FantasyTeamScore { get; set; }
    }
}
