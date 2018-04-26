using Newtonsoft.Json;
using TwitterBackUp.DTO.TimelineDtos;

namespace TwitterBackUp.DTO
{
    public class ExtendedTweetDto
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
    }
}