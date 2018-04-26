using System.Collections.Generic;
using TwitterBackUp.DTO.TimelineDtos;

namespace TwitterBackUp.Models
{
    public class TimelineViewModel
    {
        public TwitterDto Twitter { get; set; }

        public ICollection<TweetDto> Tweets { get; set; }
    }
}
