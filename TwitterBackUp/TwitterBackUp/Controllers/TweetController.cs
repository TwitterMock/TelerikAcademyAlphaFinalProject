using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TweetController : Controller
    {
        private readonly ITwitterApiProvider twitterProvider;

        public TweetController(ITwitterApiProvider twitterProvider)
        {
            this.twitterProvider = twitterProvider;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Timeline(string twitterId)
        {
            int count = 20;
            var timeline = await this.twitterProvider.GetTwitterTimelineAsync(twitterId, count);

            var model = new TimelineViewModel
            {
                Twitter = timeline.Twitter,
                Tweets = timeline.Tweets
            };

            return View(model);
        }

        [HttpGet]
        public Task<string> Html(string twitterScreenName, string tweetId)
        {
            return this.twitterProvider.GetTweetHtmlAsync(twitterScreenName, tweetId);
        }
    }
}