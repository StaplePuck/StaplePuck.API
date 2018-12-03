using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class GameDate
    {
        public string GameDateId { get; set; }
        public List<GameDateSeason> GameDateSeasons { get; set; }
        public List<TeamStateOnDate> TeamsStateOnDate { get; set; }
        public List<PlayerStatsOnDate> PlayersStatsOnDate { get; set; }

        public GameDate()
        {
            this.GameDateSeasons = new List<GameDateSeason>();
            this.PlayersStatsOnDate = new List<PlayerStatsOnDate>();
            this.TeamsStateOnDate = new List<TeamStateOnDate>();
        }
    }
}
