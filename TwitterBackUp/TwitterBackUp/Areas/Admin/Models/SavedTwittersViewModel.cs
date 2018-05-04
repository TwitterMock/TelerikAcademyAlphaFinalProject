using System.Collections.Generic;
using TwitterBackUp.Models;

namespace TwitterBackUp.Areas.Admin.Models
{
    public class SavedTwittersViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }
        
        public List<TwitterViewModel> Twitters { get; set; }
    }
}