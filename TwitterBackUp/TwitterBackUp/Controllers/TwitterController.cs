using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TwitterBackUp.DTO;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Utils.Contracts;
using System.Linq;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITwitterApiProvider twitterApiProvider;
        private readonly ITwitterRepository twitterRepository;
        private readonly ITwittersService twittersService;
        private readonly IMapper mapper;

        public TwitterController(UserManager<ApplicationUser> userManager, ITwitterApiProvider twitterApiProvider, ITwitterRepository twitterRepository, ITwittersService twittersService, IMapper mapper)
        {
            this.userManager = userManager;
            this.twitterApiProvider = twitterApiProvider;
            this.twitterRepository = twitterRepository;
            this.twittersService = twittersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string screenName)
        {
            var task = Task.Run(() => this.twitterApiProvider.GetTwitterByScreenNameAsync(screenName));

            var userId = this.userManager.GetUserId(this.User);
            var twitter = this.twitterRepository.GetSingle(screenName, userId);

            var searchedTwitter = mapper.Map<TwitterDto, TwitterViewModel>(await task);

            var model = new SearchViewModel
            {
                IsSavedTwitter = twitter != null,
                IsSuccess = searchedTwitter != null,
                SearchedTwitter = searchedTwitter,
                SearchString = screenName
            };

            return View(model);
        }

        [HttpGet]
        public Task<string> Suggestions(string category)
        {
            return this.twitterApiProvider.GetSearchSuggestionsByCategoryAsync(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(string screenName)
        {
            var twitterDto = await this.twitterApiProvider.GetTwitterByScreenNameAsync(screenName);
            var userId = this.userManager.GetUserId(this.User);
            var twitter = this.mapper.Map<TwitterDto, Twitter>(twitterDto);
            this.twittersService.SaveTwitterByUserId(userId, twitter);

            return new OkResult();
        }
        public IActionResult SavedTwitters()
        {
            var userId = this.userManager.GetUserId(this.User);
            var twitters = this.twitterRepository.GetManyByUserId(userId);

            var model = new SavedTwittersViewModel
            {
                Twitters = twitters.Select(t => this.mapper.Map<Twitter, TwitterViewModel>(t)).ToList()
            };

            return View(model);
        }
        [HttpPost]
   
        public IActionResult DeleteTwitter( string twitterId)
        {
            string userId = null;
            if (userId == null)
            {
                userId = this.userManager.GetUserId(this.User);
            }

            if (this.twitterRepository.DeleteSingleTwitter(twitterId, userId) > 0)
            {
                return new OkResult();
            }

            return new OkResult();
        }
    
    }
}