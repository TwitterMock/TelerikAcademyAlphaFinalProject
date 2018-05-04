using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.Controllers;
using TwitterBackUp.Models;

namespace TwitterBackUp.ControllersTests.TwitterControllerTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void asd()
        {
            var model = new TwitterViewModel
            {
                TwitterId = "12312",
                Username = "cheficha"
            };
            string username = "cheficha";

            MyController<TwitterController>
                .Instance()
                .WithSetup(controller => controller.ModelState
                    .AddModelError("TestError", "TestErrorMessage"))
                .Calling(c => c.Search(username))
                .ShouldReturn()
                .View()
                .AndAlso()
                .ShouldPassForThe<ViewResult>(viewResult => Assert.AreSame(model, viewResult.Model));

        }
    }
}
