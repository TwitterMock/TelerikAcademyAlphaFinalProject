using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Repositories
{
    public class TweetRepository : GenericRepository<Tweet, string>, ITweetRepository
    {
        public TweetRepository(TwitterContext context) : base(context)
        {
        }

        public ICollection<Tweet> GetManyByUserId(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var param = new SqlParameter("@userId", id);
            return this.DbSet.FromSql("GetTweetsByUserId @userId", param).ToList();
        }

        public Tweet GetSingle(string tweetId, string userId)
        {
            if (tweetId == null) throw new ArgumentNullException(nameof(tweetId));
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            return this.DbSet.FirstOrDefault(t => t.Id == tweetId && t.UsersTweets.Any(u => u.UserId == userId));
        }

        public int DeleteSingle(string tweetId, string userId)
        {
            if (tweetId == null) throw new ArgumentNullException(nameof(tweetId));
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            var userIdParam = new SqlParameter("@UserId", userId);
            var tweetIdParam = new SqlParameter("@TweetId", tweetId);

            return this.Context.Database.ExecuteSqlCommand("DeleteTweetByUser @TweetId @UserId", userIdParam,
                tweetIdParam);
        }
    }
}