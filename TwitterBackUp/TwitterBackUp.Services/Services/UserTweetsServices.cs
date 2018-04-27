using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.DataModels.Contracts;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.DTO.TwitterTimelineDtos;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Services.Services
{
    public class UserTweetsServices:IUserTweetsServices
    {
        private readonly ITweetRepository tweetRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<UsersTweets> usersTweetsRepo;

        public UserTweetsServices(ITweetRepository tweetRepo, IUnitOfWork unitOfWork, IGenericRepository<UsersTweets> usersTweetsRepo)
        {
            this.tweetRepo = tweetRepo;
            this.unitOfWork = unitOfWork;
            this.usersTweetsRepo = usersTweetsRepo;
        }

        public void StoreTweetById(string userId, TweetDto tweetDto)
        {

            var tweet = new Tweet {
                Id = tweetDto.Id,
                Content = tweetDto.Content,
                CreatedOn = tweetDto.CreatedOn

            };


            if (userId == null || tweet == null)
            {
                throw new ArgumentNullException();
            }


            var targetTweet = this.tweetRepo.GetById(tweet.Id);

            if (targetTweet == null)
            {
                this.tweetRepo.Insert(tweet);



            }
            var tweetUserRecord = new UsersTweets
            {
                UserId = userId,
                TweetId = tweet.Id

            };
            this.usersTweetsRepo.Insert(tweetUserRecord);
            this.unitOfWork.SaveChanges();

        }
    }
}
