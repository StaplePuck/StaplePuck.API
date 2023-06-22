using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Models
{
    public class FantasyTeamChanged
    {
        public int FantasyTeamId { get; set; }
        public int OldScore { get; set; }
        public int CurrentScore { get; set; }
        public int OldRank { get; set; }
        public int CurrentRank { get; set; }
    }
}
