using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBackUp.Models
{
    public class SaveTwitterAccountViewModel
    {

       
        public string Username { get; set; }
       
        public string ScreenName { get; set; }

        public string Description { get; set; }
     
        public string Url { get; set; }
    
        public int FollowersCount { get; set; }
    
        public int FriendsCount { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileBackgroundImageUrl { get; set; }
    }
}
