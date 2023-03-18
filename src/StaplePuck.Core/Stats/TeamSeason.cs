using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class TeamSeason
    {
        public int SeasonId { get; set; }
        public Season Season { get; set; } = new Season();

        public int TeamId { get; set; }
        public Team Team { get; set; } = new Team();

        public List<PlayerSeason> PlayerSeasons { get; set; } = new List<PlayerSeason>();
    }
}
