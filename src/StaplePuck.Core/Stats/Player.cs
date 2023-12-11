using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;

namespace StaplePuck.Core.Stats
{
    public class Player
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string? ExternalId2 { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<PlayerSeason> PlayerSeasons { get; set; } = new List<PlayerSeason>();
        public int Number { get; set; }
        public List<FantasyTeamPlayers> FantasyTeamPlayers { get; set; } = new List<FantasyTeamPlayers>();
        public List<PlayerStatsOnDate> StatsOnDate { get; set; } = new List<PlayerStatsOnDate>();
        public int SportId { get; set; }
        public Sport? Sport { get; set; }
    }
}
