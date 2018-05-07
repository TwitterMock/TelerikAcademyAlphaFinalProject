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
using TwitterBackUp.Services.Utils;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITwitterRequestHandler twitterRequestHandler;
        private readonly ITwitterRepository twitterRepository;
        private readonly ITwittersService twittersService;
        private readonly IMapper mapper;

        public TwitterController(UserManager<ApplicationUser> userManager, ITwitterRequestHandler twitterRequestHandler, ITwitterRepository twitterRepository, ITwittersService twittersService, IMapper mapper)
        {
            this.userManager = userManager;
            this.twitterRequestHandler = twitterRequestHandler;
            this.twitterRepository = twitterRepository;
            this.twittersService = twittersService;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            if (searchString == null) return BadRequest();

            var task = this.twitterRequestHandler.GetTwitterByScreenNameAsync(searchString);

            var userId = this.userManager.GetUserId(this.User);
            var twitter = this.twitterRepository.GetSingle(searchString, userId);

            var searchedTwitter = mapper.Map<TwitterDto, TwitterViewModel>(await task);

            var model = new SearchViewModel
            {
                IsSavedTwitter = twitter != null,
                IsSuccess = searchedTwitter != null,
                SearchedTwitter = searchedTwitter,
                SearchString = searchString
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string category)
        {
            if (category == null) return BadRequest();

            var suggestions = await this.twitterRequestHandler.GetSearchSuggestionsByCategoryAsync(category);

            if (suggestions == null) return NotFound();

            return this.Content(suggestions, "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(string screenName)
        {
            if (screenName == null) return BadRequest();

            var twitterDto = await this.twitterRequestHandler.GetTwitterByScreenNameAsync(screenName);

            if (twitterDto == null) return NotFound();
            
            var userId = this.userManager.GetUserId(this.User);

            var twitter = this.mapper.Map<TwitterDto, Twitter>(twitterDto);

            this.twittersService.SaveTwitterByUserId(userId, twitter);

            return Json(new { twitter_id = twitter.Id });
        }

        public IActionResult Saved()
        {
            var userId = this.userManager.GetUserId(this.User);
            var twitters = this.twitterRepository.GetAllByUserId(userId);

            var model = twitters.Select(t => this.mapper.Map<Twitter, TwitterViewModel>(t)).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            if (id == null) return BadRequest();

            var userId = this.userManager.GetUserId(this.User);

            if (this.twitterRepository.DeleteSingleTwitter(id, userId) > 0)
            {
                return RedirectToAction(nameof(Saved));
            }

            return NotFound();
        }

        public IActionResult Details(string id)
        {
            if (id == null) return BadRequest();

            var twitter = this.twitterRepository.GetById(id);

            if (twitter == null) return NotFound();

            var model = this.mapper.Map<Twitter, TwitterViewModel>(twitter);

            return View(model);
        }
    }
}