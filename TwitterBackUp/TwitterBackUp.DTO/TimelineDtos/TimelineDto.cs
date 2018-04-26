using System.Collections.Generic;

namespace TwitterBackUp.DTO.TimelineDtos
{
    public class TimelineDto
    {
        public TwitterDto Twitter { get; set; }
        public ICollection<TweetDto> Tweets { get; set; }
    }
}