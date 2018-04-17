﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO
{
    public class TweetEntitiesDto
    {
        public TweetEntitiesDto()
        {
            this.Urls = new List<TweetURLsDto>();
        }
        [JsonProperty("urls")]
        public ICollection<TweetURLsDto> Urls { get; set; }

    }
}