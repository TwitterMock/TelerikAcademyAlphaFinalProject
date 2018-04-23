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

        public async Task<IActionResult> SearchTwitter(string screenName)
        {
            var userResult = await twitterProvider.SearchUser(screenName);
            var model = mapper.Map<TwitterSearchDto, TwitterSearchViewModel>(userResult);

            return View(model);
        }
    }
}