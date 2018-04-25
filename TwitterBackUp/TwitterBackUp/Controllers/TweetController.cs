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
    }
}