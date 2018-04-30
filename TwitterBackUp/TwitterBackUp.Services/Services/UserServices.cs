﻿using Microsoft.AspNetCore.Identity;
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


namespace TwitterBackUp.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITweetRepository tweetRepo;

        public UserServices(UserManager<ApplicationUser> userManager, ITweetRepository tweetRepo)
        {
            this.userManager = userManager;
            this.tweetRepo = tweetRepo;
        }
        public async Task<List<ApplicationUser>> getAllUsers()
        {
            var admins = await this.userManager.GetUsersInRoleAsync("Administrator");

            var allUsers = this.userManager.Users.ToList();
            foreach (var item in admins)
            {
                if (allUsers.Contains(item))
                {
                    allUsers.Remove(item);
                }
            }

            return allUsers;
        }
        public async Task<string> DeleteUserAsync(string Id)
        {
            var user = await this.userManager.FindByIdAsync(Id);
            await this.userManager.DeleteAsync(user);
            return "deleted";

        }
        public async Task<string> PromoteUserAsync(string Id)
        {
            var user = await this.userManager.FindByIdAsync(Id);
            if (user == null)
            {
                throw new ArgumentException();
            }

            await this.userManager.AddToRoleAsync(user, "Administrator");
            return "success";
        }

    }
}
