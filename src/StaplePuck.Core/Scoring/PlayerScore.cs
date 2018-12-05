using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Scoring
{
    public class PlayerScore
    {
        public Player Player { get; set; }
        public Team Team { get; set; }
        public List<ScoreItem> Scoring { get; set; }
        public int Count { get; set; }
        public GameState GameState { get; set; }

        public int Score
        {
            get { return Scoring.Sum(x => x.Score); }
        }
        public int TodaysScore
        {
            get { return Scoring.Sum(x => x.TodaysScore); }
        }
    }
}
