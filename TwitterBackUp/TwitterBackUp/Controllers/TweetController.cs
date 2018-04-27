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
using TwitterBackUp.DTO.TwitterTimelineDtos;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TweetController : Controller
    {
        private readonly ITwitterApiProvider twitterProvider;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserTweetsServices tweetsServices;

        public TweetController(ITwitterApiProvider twitterProvider,UserManager<ApplicationUser> userManager,IUserTweetsServices tweetsServices,IMapper mapper)
        {
            this.twitterProvider = twitterProvider;
            this.userManager = userManager;
            this.tweetsServices = tweetsServices;
            this.mapper = mapper;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> GetTwitterTimeline(string id)
        {
            int count = 20;
            var timeline = await this.twitterProvider.GetUserTimeLine(id, count);

            var model = new TwitterTimelineViewModel
            {
                Twitter = timeline.Twitter,
                Tweets = timeline.Tweets
            };

            return View(model);
        }

        [HttpGet]
        public Task<string> GetTweetHtml([FromQuery]string twitterScreenName, [FromQuery]string tweetId)
        {
            return this.twitterProvider.GetTweetHtml(twitterScreenName, tweetId);
        }
        [HttpPost]
        public async Task<IActionResult> SaveUserTweet(string userTweetId)
        {
            try
            {

          
            var userId = this.userManager.GetUserId(this.User);

            var tweet = await this.twitterProvider.SearchTweetById(userTweetId);

           

                  this.tweetsServices.StoreTweetById(userId, tweet);

                var model = mapper.Map<TweetDto, SaveTweetViewModel>(tweet);

                return PartialView("_SaveTweetPartial",model);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.InnerException.Message);

            }
        }
    }
}