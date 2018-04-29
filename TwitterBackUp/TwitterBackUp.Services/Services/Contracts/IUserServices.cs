using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;

namespace TwitterBackUp.Services.Services.Contracts
{
   public  interface IUserServices
    {
        List <ApplicationUser> getAllUsers();
        Task<string> PromoteUser(string Id);
    }
}
