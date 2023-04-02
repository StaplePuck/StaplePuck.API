using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StaplePuck.Core.Models
{
    public class ResultModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
