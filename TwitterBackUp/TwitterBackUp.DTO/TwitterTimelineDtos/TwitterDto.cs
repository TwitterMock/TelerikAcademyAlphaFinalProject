using Newtonsoft.Json;

namespace TwitterBackUp.DTO.TwitterTimelineDtos
{
    public class TwitterDto
    {
        [JsonProperty("id_str")]
        public string UserId { get; set; }
        [JsonProperty("name")]
        public string Username { get; set; }
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}