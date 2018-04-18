using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO.SingleUser
{
    public class SingleUserDto
    {
        [JsonProperty("id_str")]
        public string UserId { get; set; }
        [JsonProperty("screen_name")]
        public string Username { get; set; }
        [JsonProperty("profile_image_url")]
        public string ProfileImage { get; set; }
    }
}
