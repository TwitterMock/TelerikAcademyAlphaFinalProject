using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult SearchUser(string screenName)
        {
            return View();
        }
    }
}