using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Controllers
{
    public class SearchSuggestionsController:Controller
    {
        private readonly ITwitterApiProvider twitterProvider;

        public SearchSuggestionsController(ITwitterApiProvider twitterProvider)
        {
            this.twitterProvider = twitterProvider;
        }


        [HttpGet]
        public async Task<string> GetSuggestions([FromQuery]string input) {

            var result =await this.twitterProvider.GetSearchSuggestions(input);

           return result;
         
        }
    }
}
