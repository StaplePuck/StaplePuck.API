using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class PlayerStatsOnDate
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player? Player { get; set; }

        public string GameDateId { get; set; } = string.Empty;
        public GameDate? GameDate { get; set; }

        public List<PlayerScore> PlayerScores { get; set; } = new List<PlayerScore>();
    }
}
