using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackUp.DomainModels
{
    public class Tweet
    {
        public Tweet()
        {
            this.UsersTweets = new HashSet<UsersTweets>();
        }

        [Key, Required]
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required, MinLength(5), MaxLength(512)]
        public string Content { get; set; }

        public int TwitterAccountId { get; set; }

        public int? RetweetCount { get; set; }

        public string Url { get; set; }
        
        public ICollection<UsersTweets> UsersTweets { get; set; }
    }
}