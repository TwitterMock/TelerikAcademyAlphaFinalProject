using System;

namespace TwitterBackUp.Models
{
    public class TweetViewModel
    {
        public string TweetId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string TwitterId { get; set; }

        public string TwitterScreenName { get; set; }

        public int? RetweetsCount { get; set; }
    }
}