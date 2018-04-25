using Newtonsoft.Json;

namespace TwitterBackUp.DTO
{
    public class TwitterSuggestionsDto
    {
        [JsonProperty("screen_name")]
        public string ScreenNameSuggestion { get; set; }
    }
}
