using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TwitterBackUp.DTO;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using TwitterBackUp.Data.Identity;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        private readonly ITwitterApiProvider twitterProvider;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserTwittersServices userTwittersServices;
        

        public TwitterController(ITwitterApiProvider twitterProvider, IMapper mapper, UserManager<ApplicationUser> userManager, IUserTwittersServices userTwittersServices)
        {
            this.twitterProvider = twitterProvider;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userTwittersServices = userTwittersServices;
        }

        public async Task<IActionResult> SearchTwitter(string screenName)
        {
            var userResult = await twitterProvider.SearchUser(screenName);
            var model = mapper.Map<TwitterSearchDto, TwitterSearchViewModel>(userResult);

            return View(model);
        }

        [HttpGet]
        public Task<string> GetSuggestions([FromQuery]string category)
        {
            return this.twitterProvider.GetSearchSuggestionsByCategory(category);
        }
        //public IActionResult SaveTwitterAccount(Ti)
        //{
        //    this.userManager.GetUserId(this.User);

        //}
    }
}