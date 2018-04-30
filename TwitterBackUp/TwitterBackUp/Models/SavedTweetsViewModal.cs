using System.Collections.Generic;

namespace TwitterBackUp.Models
{
    public class SavedTweetsViewModal
    {
        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}