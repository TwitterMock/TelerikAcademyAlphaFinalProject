﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;
using System;

namespace TwitterBackUp.DataModels.Repositories
{
    public class TwitterRepository : GenericRepository<Twitter, string>, ITwitterRepository
    {
        public TwitterRepository(TwitterContext context) : base(context)
        {
        }

        public ICollection<Twitter> GetAllByUserId(string id)
        {
            var param = new SqlParameter("@UserId", id);
            return this.DbSet.FromSql("GetAllTwittersByUserId @UserId", param).ToList();
        }

        public Twitter GetSingle(string screenName, string userId)
        {
            return this.DbSet.FirstOrDefault(t => t.ScreenName == screenName && t.UsersTwitters.Any(u => u.UserId == userId));
        }

        public int DeleteSingleTwitter(string twitterId, string userId)
        {
            if (twitterId == null) throw new ArgumentNullException(nameof(twitterId));
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            var userIdParam = new SqlParameter("@UserId", userId);
            var twitterIdParam = new SqlParameter("@TwitterId", twitterId);

            return this.Context.Database.ExecuteSqlCommand("DeleteSingleTwitter @TwitterId, @UserId", userIdParam,
                twitterIdParam);
        }

        public int DeleteAllTwittersByUserId(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            var userIdParam = new SqlParameter("@UserId", userId);
            return this.Context.Database.ExecuteSqlCommand("DeleteAllTweetByUserId @UserId", userIdParam);
        }
    }
}