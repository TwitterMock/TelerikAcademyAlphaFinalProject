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
        private readonly IGenericRepository<UsersTweets> usersTweetsRepository;

        public TweetService(IUnitOfWork unitOfWork, ITweetRepository tweetRepository, IGenericRepository<UsersTweets> usersTweetsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.tweetRepository = tweetRepository;
            this.usersTweetsRepository = usersTweetsRepository;
        }

        public void StoreTweetByUserId(string userId, Tweet tweet)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            if (userId == string.Empty) throw new ArgumentException(nameof(userId));
            if (tweet == null) throw new ArgumentNullException(nameof(tweet));

            var tweetById = this.tweetRepository.GetById(tweet.Id);

            if (tweetById == null)
            {
                this.tweetRepository.Insert(tweet);
            }

            var tweetUserRecord = new UsersTweets { UserId = userId, TweetId = tweet.Id };
            this.usersTweetsRepository.Insert(tweetUserRecord);
            this.unitOfWork.SaveChanges();
        }
    }
}
