using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class NumberPerPosition
    {
        public int PositionTypeId { get; set; }
        public PositionType PositionType { get; set; } = new PositionType();
        public int LeagueId { get; set; }
        public League League { get; set; } = new League();

        public int Count { get; set; }
    }
}
