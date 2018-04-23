using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Repositories
{
    public class TwitterRepository : GenericRepository<Twitter>, ITwitterRepository
    {
        public TwitterRepository(TwitterContext context) : base(context)
        {
        }

        public IEnumerable<Twitter> GetTwittersByUserId(string id)
        {
            var param = new SqlParameter("@UserId", id);
            return this.DbSet.FromSql("GetTwittersByUserId @UserId", param).ToList();
        }
    }
}