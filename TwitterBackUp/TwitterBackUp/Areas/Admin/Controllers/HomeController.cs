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

namespace TwitterBackUp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public HomeController(IUserServices userServices,IMapper mapper)
        {
            this.userServices = userServices;
            this.mapper = mapper;
        }
        [Authorize(Roles ="Administrator")]
        public IActionResult Index()
        {
            return View("Index");
        }
           public IActionResult Users()
        {
            var users=this.userServices.getAllUsers();
            var allUsersAsViewModels = new List<UserViewModel>();
            foreach (var item in users)
            {
                var user = this.mapper.Map<ApplicationUser, UserViewModel>(item);
                allUsersAsViewModels.Add(user);
            }

            return View(allUsersAsViewModels);
        }
    }
}