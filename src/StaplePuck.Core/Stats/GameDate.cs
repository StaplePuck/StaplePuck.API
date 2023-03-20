using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class GameDate
    {
        public string Id { get; set; } = string.Empty;
        public List<GameDateSeason> GameDateSeasons { get; set; } = new List<GameDateSeason>();
        public List<PlayerStatsOnDate> PlayersStatsOnDate { get; set; } = new List<PlayerStatsOnDate>();
    }
}
