using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubikTangle.API.Models
{
    public class Highscore
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        public DateTime CompletionDate { get; set; }
        public int Steps { get; set; }
    }
}
