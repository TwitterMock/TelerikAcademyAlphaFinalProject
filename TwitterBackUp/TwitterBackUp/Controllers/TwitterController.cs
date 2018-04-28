using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TwitterBackUp.DTO;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        private readonly ITwitterApiProvider twitterApiProvider;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITwittersService twittersService;
   

        public TwitterController(ITwitterApiProvider twitterApiProvider, IMapper mapper, UserManager<ApplicationUser> userManager, ITwittersService twittersService)
        {
            this.twitterApiProvider = twitterApiProvider;
            this.mapper = mapper;
            this.userManager = userManager;
            this.twittersService = twittersService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string screenName)
        {
            var userResult = await this.twitterApiProvider.GetTwitterByScreenNameAsync(screenName);
            var model = mapper.Map<TwitterDto, SearchViewModel>(userResult);

            return View(model);
        }

        [HttpGet]
        public Task<string> Suggestions(string category)
        {
            return this.twitterApiProvider.GetSearchSuggestionsByCategoryAsync(category);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(string screenName)
        {
            var twitterDto = await this.twitterApiProvider.GetTwitterByScreenNameAsync(screenName);
            var userId = this.userManager.GetUserId(this.User);
            var twitter = this.mapper.Map<TwitterDto, Twitter>(twitterDto);
            this.twittersService.StoreTwitterByUserId(userId, twitter);

            return new OkResult();
        }
    }
}