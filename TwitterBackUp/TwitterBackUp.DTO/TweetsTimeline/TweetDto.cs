using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO.TweetsTimeline
{
    public class TweetDto
    {

        [JsonProperty("id")]
        public string tweetId { get; set; }

        [JsonProperty("created_at")]
        public string DateOfCreation { get; set; }

        [JsonProperty("text")]
        public string Content { get; set; }

        [JsonProperty("urls")]
        public TweetUrls Urls { get; set; }
    }
}
