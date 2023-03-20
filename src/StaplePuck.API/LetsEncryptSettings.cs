using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaplePuck.API
{
    public class LetsEncryptSettings
    {
        public string Email { get; set; }
        public string Domains { get; set; }
        public string CountryName { get; set; }
        public string Locality { get; set; }
        public string Organization { get; set; }
        public string OrganizationUnit { get; set; }
        public string State { get; set; }
    }
}
