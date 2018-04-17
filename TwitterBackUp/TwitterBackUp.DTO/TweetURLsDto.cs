using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO
{
    public class TweetURLsDto
    {
        [JsonProperty("url")]
        public string TweetUrl { get; set; }
    }
}
