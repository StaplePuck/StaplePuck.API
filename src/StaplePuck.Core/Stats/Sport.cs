using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ScoringType> ScoringTypes { get; set; } = new List<ScoringType>();
        public List<PositionType> PositionTypes { get; set; } = new List<PositionType>();
    }
}
