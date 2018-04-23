using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO
{
    public class TwitterSuggestionsDto
    {
        [JsonProperty("screen_name")]
        public string ScreenNameSuggestion { get; set; }
    }
}
