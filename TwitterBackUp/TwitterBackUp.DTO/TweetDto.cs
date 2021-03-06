﻿using Newtonsoft.Json;

namespace TwitterBackUp.DTO
{
    public class TweetDto
    {
        [JsonProperty("id")]
        public string TweetId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }

        [JsonProperty("text")]
        public string Content { get; set; }

        [JsonProperty("user")]
        public TwitterDto Twitter { get; set; }
        
        [JsonProperty("retweet_count")]
        public int RetweetsCount { get; set; }

        [JsonProperty("truncated")]
        public bool IsTruncated { get; set; }
    }
}