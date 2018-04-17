using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackUp.DTO;

namespace TwitterBackUp.Models.TweetsViewModels
{
    public class SearchTweetViewModel
    {

        public string CreatedOn { get; set; }

        public string TweetId { get; set; }

        public string TweetContent { get; set; }

        public ICollection<string> HashTags { get; set; }


        public string UserId { get; set; }

        public string Username { get; set; }
    }
}
