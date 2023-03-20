using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class GameDateSeason
    {
        public string GameDateId { get; set; }
        public GameDate GameDate { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
