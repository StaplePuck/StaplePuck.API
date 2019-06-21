using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class PlayerSeason
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int PositionTypeId { get; set; }
        public PositionType PositionType { get; set; }

        public TeamStateForSeason TeamStateForSeason { get; set; }
    }
}
