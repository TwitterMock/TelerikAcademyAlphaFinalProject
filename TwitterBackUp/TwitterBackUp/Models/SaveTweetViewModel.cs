using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Models
{
    public class SaveTweetViewModel
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string Content { get; set; }


        public int? RetweetCount { get; set; }


        public ICollection<UsersTweets> UsersTweets { get; set; }
    }
}
