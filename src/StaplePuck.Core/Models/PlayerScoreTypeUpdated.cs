using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Models
{
    public class PlayerScoreTypeUpdated
    {
        public int ScoreTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OldScore { get; set; }
        public int CurrentScore { get; set; }
    }
}
