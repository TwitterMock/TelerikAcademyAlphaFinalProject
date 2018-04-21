using System.ComponentModel.DataAnnotations;

namespace TwitterBackUp.DomainModels
{
    public class TwitterTweet
    {
        [Required]
        public string TwitterId { get; set; }
        [Required]
        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
        public Twitter Twitter { get; set; }
    }
}