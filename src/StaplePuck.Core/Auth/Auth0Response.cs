using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Auth
{
    internal class Auth0Response
    {
        public string access_token { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
        public int expires_in { get; set; }
        public string token_type { get; set; } = string.Empty;
    }
}
