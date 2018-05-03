﻿using System;
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
using TwitterBackUp.Services.Utils;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TweetController : Controller
    {
        private readonly ITwitterApiProvider twitterApiProvider;
        private readonly IMapper mapper;
        private readonly IUserManagerProvider userManager;
        private readonly ITweetService tweetServices;
        private readonly ITweetRepository tweetRepository;

        public TweetController(ITwitterApiProvider twitterApiProvider, IMapper mapper, IUserManagerProvider userManager, ITweetService tweetServices, ITweetRepository tweetRepository)
        {
            this.twitterApiProvider = twitterApiProvider;
            this.mapper = mapper;
            this.userManager = userManager;
            this.tweetServices = tweetServices;
            this.tweetRepository = tweetRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Timeline(string twitterId)
        {
            if (twitterId == null) return new BadRequestResult();

            int count = 20;
            var tweets = await this.twitterApiProvider.GetTwitterTimelineAsync(twitterId, count);

            var model = new TimelineViewModel
            {
                Tweets = tweets.Select(t => this.mapper.Map<TweetDto, TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RenderingDetails(string twitterScreenName, string tweetId)
        {
            var task = Task.Run(() => this.twitterApiProvider.GetTweetHtmlAsync(twitterScreenName, tweetId));

            var userId = this.userManager.GetUserId(this.User);
            var tweet = this.tweetRepository.GetSingle(tweetId, userId);

            var html = await task;

            return Json(new { html, isSaved = tweet != null });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var tweetDto = await this.twitterApiProvider.GetTweetByIdAsync(id);

            var tweet = this.mapper.Map<TweetDto, Tweet>(tweetDto);

            this.tweetServices.SaveTweetByUserId(userId, tweet);

            return new OkResult();
        }

        [HttpGet]
        public IActionResult Saved()
        {
           
              var  userId = this.userManager.GetUserId(this.User);
            

            var tweets = this.tweetRepository.GetManyByUserId(userId);

            var model = new SavedTweetsViewModal
            {
                Tweets = tweets.Select(t => this.mapper.Map<Tweet, TweetViewModel>(t)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string tweetId, string userId)
        {
            if (userId == null)
            {
                userId = this.userManager.GetUserId(this.User);
            }

            if (this.tweetRepository.DeleteSingle(tweetId, userId) > 0)
            {
                return new OkResult();
            }

            return new OkResult();
        }
    }
}