using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core.Data
{
    public class ResultModel
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
