using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Fantasy
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool ReceiveEmails { get; set; }
        public bool ReceiveNotifications { get; set; }
        public bool ReceiveNegativeNotifications { get; set; }
        public List<FantasyTeam> FantasyTeams { get; set; } = new List<FantasyTeam>();
        public List<NotificationToken> NotificationTokens { get; set; } = new List<NotificationToken>();
    }
}
