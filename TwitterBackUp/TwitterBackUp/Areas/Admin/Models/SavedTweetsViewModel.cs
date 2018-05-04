using System.Collections.Generic;
using TwitterBackUp.Models;

namespace TwitterBackUp.Areas.Admin.Models
{
    public class SavedTweetsViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }
        
        public List<TweetViewModel> Tweets { get; set; }
    }
}