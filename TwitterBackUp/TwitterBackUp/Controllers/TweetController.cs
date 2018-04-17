using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Controllers
{
    [Authorize]
    public class TweetController : Controller
    {
        private readonly ITwitterApiProvider twitterProvider;

        public TweetController(ITwitterApiProvider twitterProvider)
        {
            this.twitterProvider = twitterProvider;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]       
        public async Task<IActionResult> Search(string text)
        {
            var l = await twitterProvider.SearchTweetsAsync(text);

            return View(l);
        }
    }
}