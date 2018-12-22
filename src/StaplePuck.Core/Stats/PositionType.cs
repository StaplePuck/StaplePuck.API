using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class PositionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
        public List<ScoringPositions> ScoringPositions { get; set; }
        //Undefined = 0,
        //Forward = 1,
        //Defense = 2,
        //Goalie = 4
    }
}
