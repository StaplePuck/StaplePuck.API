using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;
using StaplePuck.Core.Scoring;

namespace StaplePuck.Core.Fantasy
{
    public class FantasyTeamPlayers
    {
        public int FantasyTeamId { get; set; }
        public FantasyTeam FantasyTeam { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public PlayerCalculatedScore PlayerCalculatedScore { get; set; }

        public int SeasonId{ get; set; }
        public PlayerSeason PlayerSeason { get; set; }

    }
}
