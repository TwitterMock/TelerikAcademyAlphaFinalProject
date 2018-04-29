using System;
using TwitterBackUp.DataModels.Contracts;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Services.Services
{
    public class TweetService : ITweetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITweetRepository tweetRepository;

        public TweetService(IUnitOfWork unitOfWork, ITweetRepository tweetRepository)
        {
            this.unitOfWork = unitOfWork;
            this.tweetRepository = tweetRepository;
        }

        public void SaveTweetByUserId(string userId, Tweet tweet)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            if (userId == string.Empty) throw new ArgumentException(nameof(userId));
            if (tweet == null) throw new ArgumentNullException(nameof(tweet));

            var tweetById = this.tweetRepository.GetById(tweet.Id);
            var userTweetRecord = new UsersTweets { UserId = userId, TweetId = tweet.Id };
            
            if (tweetById != null)
            {
                tweetById.UsersTweets.Add(userTweetRecord);
            }
            else
            {
                this.tweetRepository.Insert(tweet);
                tweet.UsersTweets.Add(userTweetRecord);
            }

            this.unitOfWork.SaveChanges();
        }
    }
}
