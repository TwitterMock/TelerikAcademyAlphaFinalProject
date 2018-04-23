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
    public class TweetRepository : GenericRepository<Tweet>, ITweetRepository
    {
        public TweetRepository(TwitterContext context) : base(context)
        {
        }

        public IEnumerable<Tweet> GetTweetsByUserId(string id)
        {
            var param = new SqlParameter("@UserId", id);
            return this.DbSet.FromSql("GetTweetsByUserId @UserId", param).ToList();
        }
    }
}