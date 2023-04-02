using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Stats
{
    public class Season
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public bool IsPlayoffs { get; set; }
        public List<League> Leagues { get; set; } = new List<League>();
        public List<PlayerSeason> PlayerSeasons { get; set; } = new List<PlayerSeason>();
        public List<TeamSeason> TeamSeasons { get; set; } = new List<TeamSeason>(); 
        public string ExternalId { get; set; } = string.Empty;
        public int StartRound { get; set; }
        public int SportId { get; set; }
        public Sport? Sport { get; set; }

        public List<GameDateSeason> GameDates { get; set; } = new List<GameDateSeason>();
    }
}
