using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DataModels.Contracts;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.Services
{
    public class UserTwittersServices:IUserTwittersServices
    {
        private readonly ITwitterRepository twitterRepo;
        private readonly IGenericRepository<UsersTwitters> usersTwittersRepo;
        private readonly IUnitOfWork unitOfWork;



        public UserTwittersServices(ITwitterRepository twitterRepo, IUnitOfWork unitOfWork, IGenericRepository<UsersTwitters> usersTwittersRepo)
        {
            this.twitterRepo = twitterRepo;
            this.unitOfWork = unitOfWork;
            this.usersTwittersRepo = usersTwittersRepo;
        }
        public void StoreTwitterByUserId(string userId, Twitter twitter)
        {
            if (userId==null || twitter==null)
            {
                throw new ArgumentNullException();
            }


            var twitterAccount = this.twitterRepo.GetById(twitter.Id);

            if (twitterAccount == null)
            {
                this.twitterRepo.Insert(twitter);

                
                
            }
            var twitterUserRecord = new UsersTwitters
            {
                UserId = userId,
                TwitterId = twitter.Id

            };
            this.usersTwittersRepo.Insert(twitterUserRecord);
            this.unitOfWork.SaveChanges();

        }

    }
}
