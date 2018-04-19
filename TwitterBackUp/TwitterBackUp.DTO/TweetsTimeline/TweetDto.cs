using Newtonsoft.Json;

namespace TwitterBackUp.DTO.TweetsTimeline
{
    public class TweetDto
    {

        [JsonProperty("id")]
        public string TweetId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }

        [JsonProperty("text")]
        public string Content { get; set; }

        
        public string Url { get; set; }
    }
}
