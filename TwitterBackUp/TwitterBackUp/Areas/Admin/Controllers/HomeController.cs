using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TwitterBackUp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Index()
        {
            return View("Index");
        }
           public IActionResult Users()
        {
            return View();
        }
    }
}