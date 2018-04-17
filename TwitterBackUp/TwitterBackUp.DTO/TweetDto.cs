using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO
{
    public class TweetDto
    {
        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }
        [JsonProperty("id_str")]
        public string TweetId { get; set; }
        [JsonProperty("text")]
        public string TweetContent { get; set; }
        [JsonProperty("entities")]
        public TweetEntitiesDto Entities { get; set; }
        [JsonProperty("user")]
        public TweetUserDto User { get; set; }
    }
}
