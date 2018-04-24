using System;
using Newtonsoft.Json;

namespace TwitterBackUp.DTO.TweetDtos
{
    public class TweetDto
    {
        [JsonProperty("id")]
        public string TweetId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("text")]
        public string Content { get; set; }

        [JsonProperty("user")]
        public TwitterDto Twitter { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }       

        public string Url { get; set; }
    }
}
