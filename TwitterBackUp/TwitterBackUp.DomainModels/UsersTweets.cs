using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DomainModels
{
    public class UsersTweets
    {
        public string UserId { get; set; }
        public string TweetId { get; set; }
        public Tweet Tweet { get; set; }
    }
}