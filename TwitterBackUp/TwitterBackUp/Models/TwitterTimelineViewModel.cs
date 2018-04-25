using System.Collections.Generic;
using TwitterBackUp.DTO.TwitterTimelineDtos;

namespace TwitterBackUp.Models
{
    public class TwitterTimelineViewModel
    {
        public TwitterDto Twitter { get; set; }

        public ICollection<TweetDto> Tweets { get; set; }
    }
}
