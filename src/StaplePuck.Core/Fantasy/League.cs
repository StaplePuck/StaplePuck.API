using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public int CommissionerId { get; set; }
        public User Commissioner { get; set; }

        public List<FantasyTeam> FantasyTeams { get; set; }
        public List<ScoringRulePoints> ScoringRules { get; set; }
        public List<LeagueMail> LeagueMails { get; set; }
        public string Password { get; set; }
        public string Announcement { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public string PaymentInfo { get; set; }
        public bool AllowMultipleTeams { get; set; }

        public List<NumberPerPosition> NumberPerPositions { get; set; }

        public int PlayersPerTeam { get; set; }
    }
}
