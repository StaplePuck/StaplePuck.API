using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class GameDateSeason
    {
        public string GameDateId { get; set; } = string.Empty;
        public GameDate GameDate { get; set; } = new GameDate();
        public int SeasonId { get; set; }
        public Season Season { get; set; } = new Season();
    }
}
