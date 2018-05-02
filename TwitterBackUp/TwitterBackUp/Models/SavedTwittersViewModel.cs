using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBackUp.Models
{
    public class SavedTwittersViewModel
    {
        public string UserId { get; set; }
        public ICollection<TwitterViewModel> Twitters { get; set; }

    }
}
