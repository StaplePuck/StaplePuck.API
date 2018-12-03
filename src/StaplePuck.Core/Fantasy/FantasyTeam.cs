using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Fantasy
{
    public class FantasyTeam
    {
        public int FantasyTeamId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public bool IsPaid { get; set; }
        public bool IsValid { get; set; }

        public bool ReceiveEmails { get; set; }
        public List<FantasyTeamPlayers> FantasyTeamPlayers { get; set; }
    }
}
