using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackUp.DTO
{
    public class SingleTweetDTO
    {
        DateTime CreatedOn { get; set; }
        string TweetId { get; set; }
        string TweetContent { get; set; }
        string[] TweetHashTags { get; set; }
        string UserId { get; set; }
        string UserScreenName { get; set; }
     
    }
}
