using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class PlayerScore
    {
        public int Id { get; set; }

        public int PlayerStatsOnDateId { get; set; }
        public PlayerStatsOnDate PlayerStatsOnDate { get; set; }

        public int ScoringTypeId { get; set; }
        public ScoringType ScoringType { get; set; }

        public int Total { get; set; }

        public bool AdminOverride { get; set; }
    }
}
