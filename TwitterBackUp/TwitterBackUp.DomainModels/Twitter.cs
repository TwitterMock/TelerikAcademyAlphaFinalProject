using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackUp.DomainModels
{
    public class Twitter
    {
        public Twitter()
        {
            this.TwitteTweets = new HashSet<TwitterTweet>();
        }

        [Key, Required]
        public string UserId { get; set; }
        [Required, MinLength(4), MaxLength(125)]
        public string Username { get; set; }
        [Required, MinLength(4), MaxLength(125)]
        public string ScreenName { get; set; }

        [MinLength(5), MaxLength(250)]
        public string Description { get; set; }

        public string Url { get; set; }

        public int? FollowersCount { get; set; }

        public int? FriendsCount { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileBackgroundImageUrl { get; set; }

        public ICollection<TwitterTweet> TwitteTweets { get; set; }
    }
}
