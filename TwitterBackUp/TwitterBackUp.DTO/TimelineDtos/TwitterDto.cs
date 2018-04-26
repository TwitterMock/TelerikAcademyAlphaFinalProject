using Newtonsoft.Json;

namespace TwitterBackUp.DTO.TimelineDtos
{
    public class TwitterDto
    {
        [JsonProperty("id_str")]
        public string TwitterId { get; set; }
        [JsonProperty("name")]
        public string Username { get; set; }
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}