using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class FantasyTeamPlayers
    {
        public int FantasyTeamId { get; set; }
        public FantasyTeam FantasyTeam { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
