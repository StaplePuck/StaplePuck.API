using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Models
{
    public class PlayerScoreUpdated
    {
        public int PlayerId { get; set; }
        public int OldScore { get; set; }
        public int CurrentScore { get; set; }
        public IEnumerable<PlayerScoreTypeUpdated> ScoringTypesUpdated { get; set; }
    }
}
