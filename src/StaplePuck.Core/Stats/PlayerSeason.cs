using StaplePuck.Core.Scoring;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class PlayerSeason
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; } = new Player();

        public int SeasonId { get; set; }
        public Season Season { get; set; } = new Season();

        public int TeamId { get; set; }
        public Team Team { get; set; } = new Team();
        public int PositionTypeId { get; set; }
        public PositionType PositionType { get; set; } = new PositionType();
        public TeamSeason TeamSeason { get; set; } = new TeamSeason();

        public TeamStateForSeason TeamStateForSeason { get; set; } = new TeamStateForSeason();

        public List<Fantasy.FantasyTeamPlayers> FantasyTeamPlayers { get; set; } = new List<Fantasy.FantasyTeamPlayers>();
        public List<PlayerCalculatedScore> PlayerCalculatedScores { get; set; } = new List<PlayerCalculatedScore>();
    }
}
