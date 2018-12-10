using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class ScoringRulePoints
    {
        public int Id { get; set; }
        public PositionType Position { get; set; }
        public int PointsPerScore { get; set; }

        public int ScoringTypeId { get; set; }
        public ScoringType ScoringType { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
