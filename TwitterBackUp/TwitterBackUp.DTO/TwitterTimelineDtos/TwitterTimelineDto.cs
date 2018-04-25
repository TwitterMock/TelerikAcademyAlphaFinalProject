using System.Collections.Generic;

namespace TwitterBackUp.DTO.TwitterTimelineDtos
{
    public class TwitterTimelineDto
    {
        public TwitterDto Twitter { get; set; }
        public ICollection<TweetDto> Tweets { get; set; }
    }
}