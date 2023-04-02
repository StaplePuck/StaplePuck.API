using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;
using StaplePuck.Core.Scoring;

namespace StaplePuck.Core.Fantasy
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int SeasonId { get; set; }
        public Season? Season { get; set; }

        public int CommissionerId { get; set; }
        public User? Commissioner { get; set; }

        public List<FantasyTeam> FantasyTeams { get; set; } = new List<FantasyTeam>();
        public List<ScoringRulePoints> ScoringRules { get; set; } = new List<ScoringRulePoints>();
        public List<LeagueMail> LeagueMails { get; set; } = new List<LeagueMail>();
        public string? Password { get; set; }
        public string? Announcement { get; set; }
        public string? Description { get; set; }
        public bool IsLocked { get; set; }
        public string? PaymentInfo { get; set; }
        public bool AllowMultipleTeams { get; set; }
        public bool IsActive { get; set; }

        public List<NumberPerPosition> NumberPerPositions { get; set; } = new List<NumberPerPosition>();

        public int PlayersPerTeam { get; set; }

        public List<PlayerCalculatedScore> PlayerCalculatedScores { get; set; } = new List<PlayerCalculatedScore>();
    }
}
