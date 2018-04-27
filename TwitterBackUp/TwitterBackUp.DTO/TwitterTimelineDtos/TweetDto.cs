using Newtonsoft.Json;
using System.Collections.Generic;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DTO.TwitterTimelineDtos
{
    public class TweetDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }

        [JsonProperty("text")]
        public string Content { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        

        public ICollection<UsersTweets> UsersTweets { get; set; }
    }
}
