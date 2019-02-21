﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Fantasy
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public string Email { get; set; }
        public bool ReceiveEmails { get; set; }
        public List<FantasyTeam> FantasyTeams { get; set; }
    }
}
