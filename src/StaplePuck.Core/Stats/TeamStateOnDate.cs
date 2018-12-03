using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class TeamStateOnDate
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string GameDateId { get; set; }
        public GameDate GameDate { get; set; }
        public GameState GameState { get; set; }
    }
}
