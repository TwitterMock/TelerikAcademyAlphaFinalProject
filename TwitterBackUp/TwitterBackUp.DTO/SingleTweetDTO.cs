using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO
{
    public class SingleTweetDTO
    {
        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }
        [JsonProperty("id_str")]
        public string TweetId { get; set; }
        [JsonProperty("text")]
        public string TweetContent { get; set; }
        [JsonProperty("entities//hashtags")]
        public string[] TweetHashTags { get; set; }
        [JsonProperty("user//id_str")]
        public string UserId { get; set; }
        [JsonProperty("user//screen_name")]
        public string Username { get; set; }
     
    }
}
