using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Scoring
{
    public class PlayerCalculatedScore
    {
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public int LeagueId { get; set; }
        public League? League { get; set; }

        public int SeasonId { get; set; }

        public List<CalculatedScoreItem> Scoring { get; set; } = new List<CalculatedScoreItem>();
        public int NumberOfSelectedByTeams { get; set; }
        public int GameState { get; set; }

        public int Score { get; set; }
        public int TodaysScore { get; set; }

        public List<FantasyTeamPlayers> FantasyTeamPlayers { get; set; } = new List<FantasyTeamPlayers>();
        public PlayerSeason PlayerSeason { get; set; } = new PlayerSeason();
    }
}
