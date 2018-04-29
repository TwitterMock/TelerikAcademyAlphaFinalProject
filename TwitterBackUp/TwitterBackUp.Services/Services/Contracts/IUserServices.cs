using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.Data.Identity;

namespace TwitterBackUp.Services.Services.Contracts
{
   public  interface IUserServices
    {
        List <ApplicationUser> getAllUsers();
         void PromoteUser(string Id);
    }
}
