using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class PositionType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public int SportId { get; set; }
        public Sport? Sport { get; set; }
        public List<ScoringPositions> ScoringPositions { get; set; } = new List<ScoringPositions>();
    }
}
