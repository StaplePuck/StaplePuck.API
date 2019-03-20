﻿using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Scoring
{
    public class CalculatedScoreItem
    {
        public ScoringType ScoringType { get; set; }

        public int Multiplier { get; set; }
        public int Total { get; set; }
        public int TodaysTotal { get; set; }
        public int Score { get; set; }
        public int TodaysScore { get; set; }
    }
}
