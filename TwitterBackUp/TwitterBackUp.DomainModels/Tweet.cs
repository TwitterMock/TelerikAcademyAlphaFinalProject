using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DomainModels
{
    public class Tweet : IIdentifiable<string>
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

        public string TwitterId { get; set; }

        public string TwitterScreenName { get; set; }

        public int? RetweetsCount { get; set; }

        public string Url { get; set; }
        
        public ICollection<UsersTweets> UsersTweets { get; set; }
    }
}