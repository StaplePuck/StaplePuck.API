using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Scoring
{
    public class CalculatedScoreItem
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; } = new Player();

        public int LeagueId { get; set; }
        public League League { get; set; } = new League();

        public PlayerCalculatedScore PlayerCalculatedScore { get; set; } = new PlayerCalculatedScore();

        public int ScoringTypeId { get; set; }
        public ScoringType ScoringType { get; set; } = new ScoringType();

        public int Total { get; set; }
        public int TodaysTotal { get; set; }
        public int Score { get; set; }
        public int TodaysScore { get; set; }
    }
}
