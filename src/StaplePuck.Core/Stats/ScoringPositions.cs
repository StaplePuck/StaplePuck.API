using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class ScoringPositions
    {
        public int ScoringTypeId { get; set; }
        public ScoringType? ScoringType { get; set; }
        public int PositionTypeId { get; set; }
        public PositionType? PositionType { get; set; }
    }
}
