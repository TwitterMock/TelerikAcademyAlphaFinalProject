using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Areas.Admin.Models;
using AutoMapper;
using TwitterBackUp.Data.Identity;
using Microsoft.AspNetCore.Identity;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IUserServices userServices, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.userServices = userServices;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View("Index");
        }
        public async Task<IActionResult> Users()
        {
            var users = await this.userServices.getAllUsers();
            var allUsersAsViewModels = new List<UserViewModel>();
            foreach (var item in users)
            {
                var user = this.mapper.Map<ApplicationUser, UserViewModel>(item);
                allUsersAsViewModels.Add(user);
            }

            return View(allUsersAsViewModels);
        }
        public async Task<IActionResult> PromoteUser(string Id)
        {


            await this.userServices.PromoteUserAsync(Id);

            return new OkResult();
        }
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await this.userServices.DeleteUserAsync(Id);
            return new OkResult();
        }
        //public IActionResult UserTweets(string Id)
        //{
           
          

        //}
    }
}