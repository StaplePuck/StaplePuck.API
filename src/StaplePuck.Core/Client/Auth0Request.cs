using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Client
{
    internal class Auth0Request
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string audience { get; set; }
        public string grant_type { get; set; }

        public Auth0Request(StaplePuckSettings settings)
        {
            client_id = settings.ClientId;
            client_secret = settings.ClientSecret;
            audience = settings.Audience;
            grant_type = "client_credentials";
        }
    }
}
