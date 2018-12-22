﻿using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Core.Stats
{
    public class Player
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int ExternalId { get; set; }
        public string ShortName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PositionType Position { get; set; }
        public List<PlayerSeason> PlayerSeasons { get; set; }
        public int Number { get; set; }
        public List<FantasyTeamPlayers> FantasyTeamPlayers { get; set; }
        public List<PlayerStatsOnDate> StatsOnDate { get; set; }

        public string FullNameWithPosition
        {
            get
            {
                return string.Format("{0} ({1})", FullName, this.Position.Name);
            }
        }
    }
}
