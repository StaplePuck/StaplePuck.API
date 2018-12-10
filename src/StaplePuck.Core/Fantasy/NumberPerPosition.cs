using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class NumberPerPosition
    {
        public int Id { get; set; }
        public PositionType Position { get; set; }
        public int Count { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
