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
using TwitterBackUp.DataModels.Repositories.Contracts;
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
        private readonly ITweetRepository tweetRepository;

        public TweetController(ITwitterApiProvider twitterApiProvider, IMapper mapper, UserManager<ApplicationUser> userManager, ITweetService tweetServices, ITweetRepository tweetRepository)
        {
            this.twitterApiProvider = twitterApiProvider;
            this.mapper = mapper;
            this.userManager = userManager;
            this.tweetServices = tweetServices;
            this.tweetRepository = tweetRepository;
        }

        [HttpGet]
        public async Task<IActionResult> TwitterTimeline(string twitterScreenName)
        {
            if (twitterScreenName == null) return BadRequest();

            int count = 20;
            var tweets = await this.twitterApiProvider.GetTwitterTimelineAsync(twitterScreenName, count);

            if (tweets == null) return NotFound();

            var model = new TimelineViewModel
            {
                TwitterScreenName = twitterScreenName,
                Tweets = tweets.Select(t => this.mapper.Map<TweetDto, TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RenderingDetails(string url, string id)
        {
            if (url == null) return BadRequest();
            if (id == null) return BadRequest();

            var task = Task.Run(() => this.twitterApiProvider.GetTweetHtmlAsync(url));

            var userId = this.userManager.GetUserId(this.User);
            var tweet = this.tweetRepository.GetSingle(id, userId);

            var html = await task;

            if (html == null) return NotFound();

            return Json(new { html, isSaved = tweet != null });
        }

        [HttpGet]
        public async Task<IActionResult> Html(string url)
        {
            if (url == null) return BadRequest();

            var html = await this.twitterApiProvider.GetTweetHtmlAsync(url);

            if (html == null) return NotFound();

            return Json(new { html });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(string id)
        {
            if (id == null) return BadRequest();

            var userId = this.userManager.GetUserId(this.User);

            var tweetDto = await this.twitterApiProvider.GetTweetByIdAsync(id);

            if (tweetDto == null) return NotFound();

            var tweet = this.mapper.Map<TweetDto, Tweet>(tweetDto);

            this.tweetServices.SaveTweetByUserId(userId, tweet);

            return Json(new { tweet_id = tweet.Id });
        }

        [HttpGet]
        public IActionResult Saved()
        {
            var userId = this.userManager.GetUserId(this.User);

            var tweets = this.tweetRepository.GetAllByUserId(userId);

            var model = tweets.Select(t => this.mapper.Map<Tweet, TweetViewModel>(t)).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            if (id == null) return BadRequest();

            var userId = this.userManager.GetUserId(this.User);

            if (this.tweetRepository.DeleteSingle(id, userId) > 0)
            {
                return RedirectToAction(nameof(Saved));
            }

            return NotFound();
        }
    }
}