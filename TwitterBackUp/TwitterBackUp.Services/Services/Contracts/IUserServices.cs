using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Services.Services.Contracts
{
   public  interface IUserServices
    {
        ICollection<ApplicationUser> GetAllUsers();
        Task<string> PromoteUserAsync(string id);
        Task<string> DeleteUserAsync(string id);
    }
}
