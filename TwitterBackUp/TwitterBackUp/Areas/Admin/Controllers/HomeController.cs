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
using TwitterBackUp.DataModels.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.Services.Utils;

namespace TwitterBackUp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;
        private readonly IUserManagerProvider userManager;
        private readonly ITweetRepository tweetRepo;
        private readonly ITwitterRepository twitterRepo;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUserServices userServices, IMapper mapper, IUserManagerProvider userManager, ITweetRepository tweetRepo, ITwitterRepository twitterRepo, IUnitOfWork unitOfWork)
        {
            this.userServices = userServices;
            this.mapper = mapper;
            this.userManager = userManager;
            this.tweetRepo = tweetRepo;
            this.twitterRepo = twitterRepo;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await this.userServices.getAllUsers();

            var model = users.Select(u => this.mapper.Map<ApplicationUser, UserViewModel>(u)).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteUser(string id)
        {
            if (id == null) return this.BadRequest();

            await this.userServices.PromoteUserAsync(id);

            return this.RedirectToAction("Users");
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
        public IActionResult SavedTweets(string userId)
        {
            if (userId == null) return this.BadRequest();

            var user = this.userManager.GetById(userId);

            if (user == null) return this.NotFound();

            var tweets = this.tweetRepo.GetAllByUserId(userId);

            var model = new SavedTweetsViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Tweets = tweets.Select(t => this.mapper.Map<Tweet, TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult SavedTwitters(string userId)
        {
            if (userId == null) return this.BadRequest();

            var user = this.userManager.GetById(userId);

            if (user == null) return this.NotFound();

            var twitters = this.twitterRepo.GetAllByUserId(userId);

            var model = new SavedTwittersViewModel()
            {
                UserId = user.Id,
                Username = user.UserName,
                Twitters = twitters.Select(t => this.mapper.Map<Twitter, TwitterViewModel>(t)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTwitter(string userId, string twitterId)
        {
            if (userId == null) return this.BadRequest();
            if (twitterId == null) return this.BadRequest();

            var twitter = this.twitterRepo.GetById(twitterId);

            if (twitter == null) return this.NotFound();

            this.twitterRepo.Delete(twitter);

            this.unitOfWork.SaveChanges();

            return this.RedirectToAction("SavedTwitters", new { userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTweet(string userId, string tweetId)
        {
            if (userId == null) return this.BadRequest();
            if (tweetId == null) return this.BadRequest();

            var tweet = this.tweetRepo.GetById(tweetId);

            if (tweet == null) return this.NotFound();

            this.tweetRepo.Delete(tweet);

            this.unitOfWork.SaveChanges();

            return this.RedirectToAction("SavedTweets", new { userId });
        }
    }
}