using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Client2
{
    public class StaplePuck2Exception : Exception
    {
        public StaplePuck2Exception(string message) : base(message) { }

        public StaplePuck2Exception(string message, Exception innerException) : base(message, innerException) { }
    }
}
