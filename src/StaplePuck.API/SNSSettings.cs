using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaplePuck.API
{
    public class SNSSettings
    {
        public string StatsUpdatedTopicARN { get; set; }
        public string ScoreUpdatedTopicARN { get; set; }
    }
}
