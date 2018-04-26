using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TwitterBackUp.DTO;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        private readonly ITwitterApiProvider twitterProvider;
        private readonly IMapper mapper;

        public TwitterController(ITwitterApiProvider twitterProvider, IMapper mapper)
        {
            this.twitterProvider = twitterProvider;
            this.mapper = mapper;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Search(string screenName)
        {
            var userResult = await twitterProvider.GetTwitterByScreenNameAsync(screenName);
            var model = mapper.Map<ExtendedTwitterDto, TwitterViewModel>(userResult);

            return View(model);
        }

        [HttpGet]
        public Task<string> Suggestions(string category)
        {
            return this.twitterProvider.GetSearchSuggestionsByCategoryAsync(category);
        }
    }
}