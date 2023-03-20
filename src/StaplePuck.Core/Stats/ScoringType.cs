using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class ScoringType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<ScoringPositions> ScoringPositions { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}
