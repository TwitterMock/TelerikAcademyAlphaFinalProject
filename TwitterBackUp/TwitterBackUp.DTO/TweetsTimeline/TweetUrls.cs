using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO.TweetsTimeline
{
    public class TweetUrls
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
