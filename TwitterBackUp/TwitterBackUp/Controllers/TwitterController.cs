using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwitterBackUp.Services.Services.Contracts;


namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        private readonly ITwitterApiProvider twitterProvider;

        public TwitterController(ITwitterApiProvider twitterProvider)
        {
            this.twitterProvider = twitterProvider;
        }

        public async Task<IActionResult> SearchUser(string screenName)
        {
            var search = await twitterProvider.SearchUser(screenName);
            return View(search);
        }
    }
}