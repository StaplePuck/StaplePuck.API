using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int ExternalId { get; set; }
        public List<TeamStateOnDate> TeamStateOnDates { get; set; }
    }
}
