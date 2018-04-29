using System;
using TwitterBackUp.DataModels.Contracts;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Services.Services
{
    public class TwitterService : ITwittersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITwitterRepository twitterRepository;

        public TwitterService(IUnitOfWork unitOfWork, ITwitterRepository twitterRepository)
        {
            this.unitOfWork = unitOfWork;
            this.twitterRepository = twitterRepository;
        }

        public void SaveTwitterByUserId(string userId, Twitter twitter)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            if (userId == string.Empty) throw new ArgumentException(nameof(userId));
            if (twitter == null) throw new ArgumentNullException(nameof(twitter));
            
            var twitterById = this.twitterRepository.GetById(twitter.Id);
            var userTwitterRecord = new UsersTwitters { UserId = userId, TwitterId = twitter.Id };

            if (twitterById != null)
            {
                twitterById.UsersTwitters.Add(userTwitterRecord);
            }
            else
            {
                this.twitterRepository.Insert(twitter);
                twitter.UsersTwitters.Add(userTwitterRecord);
            }
            
            this.unitOfWork.SaveChanges();
        }
    }
}
