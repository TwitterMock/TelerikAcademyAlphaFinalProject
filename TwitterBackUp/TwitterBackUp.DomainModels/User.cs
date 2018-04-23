using System.Collections.Generic;

namespace TwitterBackUp.DomainModels
{
    public class User
    {
        public User()
        {
            this.UsersTwitters = new HashSet<UsersTwitters>();
            this.UsersTweets = new HashSet<UsersTweets>();
        }

        public string Id { get; set; }

        public ICollection<UsersTwitters> UsersTwitters { get; set; }
        public ICollection<UsersTweets> UsersTweets { get; set; }
    }
}