using Newtonsoft.Json;

namespace TwitterBackUp.DTO.TweetsTimeline
{
    public class TweetUrls
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
