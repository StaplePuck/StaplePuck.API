using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class ScoringRulePoints
    {
        public int PositionTypeId { get; set; }
        public PositionType? PositionType { get; set; }
        public int PointsPerScore { get; set; }
        public double ScoringWeight { get; set; }

        public int ScoringTypeId { get; set; }
        public ScoringType? ScoringType { get; set; }
        public int LeagueId { get; set; }
        public League? League { get; set; }
    }
}
