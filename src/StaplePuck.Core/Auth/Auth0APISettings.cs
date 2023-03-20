using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Auth
{
    public class Auth0APISettings
    {
        public string ClientSecret { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;


        public string Authority
        {
            get
            {
                return $"https://{Domain}/";
            }
        }
    }
}
