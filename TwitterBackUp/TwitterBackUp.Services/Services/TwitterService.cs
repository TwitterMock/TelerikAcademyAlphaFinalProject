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
        private readonly IGenericRepository<UsersTwitters> usersTwittersRepository;

        public TwitterService(IUnitOfWork unitOfWork, ITwitterRepository twitterRepository, IGenericRepository<UsersTwitters> usersTwittersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.twitterRepository = twitterRepository;
            this.usersTwittersRepository = usersTwittersRepository;
        }

        public void StoreTwitterByUserId(string userId, Twitter twitter)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            if (userId == string.Empty) throw new ArgumentException(nameof(userId));
            if (twitter == null) throw new ArgumentNullException(nameof(twitter));
            
            var twitterById = this.twitterRepository.GetById(twitter.Id);

            if (twitterById == null)
            {
                this.twitterRepository.Insert(twitter);
            }

            var twitterUserRecord = new UsersTwitters {UserId = userId, TwitterId = twitter.Id};
            this.usersTwittersRepository.Insert(twitterUserRecord);
            this.unitOfWork.SaveChanges();
        }
    }
}
