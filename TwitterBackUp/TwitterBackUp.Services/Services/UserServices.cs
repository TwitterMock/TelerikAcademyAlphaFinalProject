﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.Services.Services.Contracts;


namespace TwitterBackUp.Services.Services
{
    public class UserServices:IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserServices(UserManager<ApplicationUser>userManager)
        {
            this.userManager = userManager;
        }
        public List<ApplicationUser> getAllUsers()
        {
            var allUsers=this.userManager.Users.ToList();

            return allUsers;
        }
    }
}
