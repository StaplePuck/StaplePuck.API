﻿using System;
using System.Collections.Generic;
using System.Text;
using StaplePuck.Core.Stats;

namespace StaplePuck.Core.Fantasy
{
    public class LeagueMail
    {
        public int LeagueMailId { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public string GameDateId { get; set; }
        public GameDate GameDate { get; set; }

        public string Message { get; set; }
    }
}
