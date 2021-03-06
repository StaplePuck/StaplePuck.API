﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LocationName { get; set; }
        public int ExternalId { get; set; }
    }
}
