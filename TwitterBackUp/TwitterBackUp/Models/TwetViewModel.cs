using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBackUp.Models
{
    public class TwetViewModel
    {
        public string tweetId { get; set; }

        public string DateOfCreation { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }
    }
}
