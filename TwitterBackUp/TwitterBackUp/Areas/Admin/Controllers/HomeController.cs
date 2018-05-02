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
using TwitterBackUp.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;

namespace TwitterBackUp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITweetRepository tweetRepo;
        private readonly ITwitterRepository twitterRepo;

        public HomeController(IUserServices userServices, IMapper mapper, UserManager<ApplicationUser> userManager,ITweetRepository tweetRepo,ITwitterRepository twitterRepo)
        {
            this.userServices = userServices;
            this.mapper = mapper;
            this.userManager = userManager;
            this.tweetRepo = tweetRepo;
            this.twitterRepo = twitterRepo;
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

            try
            {
                await this.userServices.DeleteUserAsync(Id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
            
        }
        [HttpGet]
        public IActionResult SavedForAdmin(string userId)
        {        

            var tweets = this.tweetRepo.GetManyByUserId(userId);

            var model = new SavedTweetsViewModal
            {
                Tweets = tweets.Select(t => this.mapper.Map<Tweet, TweetViewModel>(t)).ToList()
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult SavedTwittersForAdmin(string userId)
        {
            var twitters = this.twitterRepo.GetManyByUserId(userId);

            var model = new SavedTwittersViewModel
            {
                Twitters = twitters.Select(t => this.mapper.Map<Twitter, TwitterViewModel>(t)).ToList()
            };
            model.UserId = userId;
            return View(model);
        }
        [HttpPost]
       
        public IActionResult DeleteTwitterAdmin([FromQuery]string userId, [FromQuery]string twitterId)
        {
            
          
            if (userId==null)
            {
                throw new ArgumentNullException();
            }
            if (twitterId==null)
            {
                throw new ArgumentNullException();
            }

            if (this.twitterRepo.DeleteSingleTwitter(twitterId, userId) > 0)
            {
                return new OkResult();
            }

            return new OkResult();
          
        }
    }
}