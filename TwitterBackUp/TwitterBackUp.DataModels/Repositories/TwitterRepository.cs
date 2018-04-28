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
    public class TwitterRepository : GenericRepository<Twitter, string>, ITwitterRepository
    {
        public TwitterRepository(TwitterContext context) : base(context)
        {
        }

        public ICollection<Twitter> GetManyByUserId(string id)
        {
            var param = new SqlParameter("@UserId", id);
            return this.DbSet.FromSql("GetTwittersByUserId @UserId", param).ToList();
        }

        public Twitter GetSingle(string twitterId, string userId)
        {
            return this.DbSet.FirstOrDefault(t => t.Id == twitterId && t.UsersTwitters.Any(u => u.UserId == userId));
        }
    }
}