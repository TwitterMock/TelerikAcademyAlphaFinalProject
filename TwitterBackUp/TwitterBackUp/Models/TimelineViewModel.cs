using System.Collections.Generic;

namespace TwitterBackUp.Models
{
    public class TimelineViewModel
    {
        public ICollection<TweetViewModel> Tweets { get; set; }
        public string TwitterScreenName { get; set; }
    }
}
