using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Services.Utils;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Services.Services
{
    public class UserServices : IUserServices
    {
      
        private readonly ITweetRepository tweetRepo;
        private readonly IUserManagerProvider userManager;

        public UserServices(ITweetRepository tweetRepo, IUserManagerProvider userManager)
        {
           
            this.tweetRepo = tweetRepo;
            this.userManager = userManager;
        }

        public ICollection<ApplicationUser> GetAllUsers()
        {
            return this.userManager.GetAllUsers().ToList();
        }
        public async Task<string> DeleteUserAsync(string Id)
        {
            if (Id==null)
            {
                throw new ArgumentNullException();
            }
     
            var user = await this.userManager.FindByIdAsync(Id);
            this.tweetRepo.DeleteAllTweetsByUserId(Id);
            await this.userManager.DeleteUserAsync(user);

            return "deleted";

        }
        public async Task<string> PromoteUserAsync(string Id)
        {
            if (Id==null)
            {
                throw new ArgumentException();
            }

            var user = await this.userManager.FindByIdAsync(Id);

            if (user == null)
            {
                throw new ArgumentException();
            }
            await this.userManager.DeleteFromRoleAsync(user,"User");
            await this.userManager.AddToRoleAsync(user, "Administrator");
            return "success";
        }

    }
}
