using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DomainModels;
using TwitterBackUp.DTO;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TweetController : Controller
    {
        private readonly ITwitterApiProvider twitterApiProvider;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITweetService tweetServices;

        public TweetController(ITwitterApiProvider twitterApiProvider, UserManager<ApplicationUser> userManager, ITweetService tweetsServices, IMapper mapper)
        {
            this.twitterApiProvider = twitterApiProvider;
            this.userManager = userManager;
            this.tweetServices = tweetsServices;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Timeline(string twitterId)
        {
            int count = 20;
            var tweets = await this.twitterApiProvider.GetTwitterTimelineAsync(twitterId, count);

            var model = new TimelineViewModel
            {
                Tweets = tweets.Select(t => this.mapper.Map<TweetDto, TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public Task<string> Html(string twitterScreenName, string tweetId)
        {
            return this.twitterApiProvider.GetTweetHtmlAsync(twitterScreenName, tweetId);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAsync(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var tweetDto = await this.twitterApiProvider.GetTweetByIdAsync(id);
 
            var tweet = this.mapper.Map<TweetDto, Tweet>(tweetDto);
            this.tweetServices.StoreTweetByUserId(userId, tweet);

            return new OkResult();
        }
    }
}