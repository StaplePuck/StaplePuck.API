using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaplePuck.API.Auth
{
    public class Auth0Settings
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
