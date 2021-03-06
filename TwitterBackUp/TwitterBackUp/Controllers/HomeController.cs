﻿using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterBackUp.Models;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITwitterRequestHandler twitterRequestHandler;

        public HomeController(ITwitterRequestHandler twitterRequestHandler)
        {
            this.twitterRequestHandler = twitterRequestHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
