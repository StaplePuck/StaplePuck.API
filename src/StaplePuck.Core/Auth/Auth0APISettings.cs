using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Auth
{
    public class Auth0APISettings
    {
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
        public string Audience { get; set; }
        public string Domain { get; set; }


        public string Authority
        {
            get
            {
                return $"https://{Domain}/";
            }
        }
    }
}
