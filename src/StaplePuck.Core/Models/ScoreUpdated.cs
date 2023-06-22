using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Models
{
    public class ScoreUpdated
    {
        public int LeagueId { get; set; }
        public IEnumerable<PlayerScoreUpdated> PlayersScoreUpdated { get; set; } = new List<PlayerScoreUpdated>();
        public IEnumerable<FantasyTeamChanged> FantasyTeamChanges { get; set; } = new List<FantasyTeamChanged>();
    }
}
