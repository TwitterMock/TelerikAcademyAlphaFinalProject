using Newtonsoft.Json;

namespace TwitterBackUp.DTO
{
    public class ExtendedTwitterDto
    {
        [JsonProperty("id_str")]
        public string TwitterId { get; set; }
        [JsonProperty("name")]
        public string Username { get; set; }
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }
        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }
        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("profile_background_image_url")]
        public string ProfileBackgroundImageUrl { get; set; }
    }
}