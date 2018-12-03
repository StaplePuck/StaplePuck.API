using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Fantasy
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<FantasyTeam> FantasyTeams { get; set; }
    }
}
