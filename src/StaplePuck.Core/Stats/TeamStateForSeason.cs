using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class TeamStateForSeason
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        public int GameState { get; set; }
        public List<PlayerSeason> PlayerSeasons { get; set; }
    }
}
