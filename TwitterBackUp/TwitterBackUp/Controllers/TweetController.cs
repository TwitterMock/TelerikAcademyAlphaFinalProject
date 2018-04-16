using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TwitterBackUp.Controllers
{
 
    
    public class TweetController : Controller
    {
        [HttpPost]
        [AutoValidateAntiforgeryToken]       
        public IActionResult Search(string text)
        {
            ViewData["text"]=text;

            return View();
        }
    }
}