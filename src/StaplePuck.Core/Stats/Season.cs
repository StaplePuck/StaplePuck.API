using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Stats
{
    public class Season
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public bool IsPlayoffs { get; set; }
        public List<League> Leagues { get; set; }
        public List<PlayerSeason> PlayerSeasons { get; set; }
        public List<TeamSeason> TeamSeasons { get; set; }
        public string ExternalId { get; set; }
        public int StartRound { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }

        public List<GameDateSeason> GameDates { get; set; }
    }
}
