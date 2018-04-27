using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.DomainModels;
using TwitterBackUp.DTO.TwitterTimelineDtos;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface IUserTweetsServices
    {
        void StoreTweetById(string userId, TweetDto tweet);
    }
}
