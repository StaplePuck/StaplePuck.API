using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Stats
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public int ExternalId { get; set; }
        public string? ExternalId2 { get; set; }
    }
}
