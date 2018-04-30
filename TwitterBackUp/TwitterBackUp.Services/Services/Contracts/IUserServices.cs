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
        Task<List <ApplicationUser>> getAllUsers();
        Task<string> PromoteUserAsync(string Id);
        Task<string> DeleteUserAsync(string Id);
    }
}
