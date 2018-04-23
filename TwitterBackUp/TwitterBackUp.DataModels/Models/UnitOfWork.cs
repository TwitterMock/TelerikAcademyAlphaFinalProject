using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TwitterBackUp.DataModels.Contracts;

namespace TwitterBackUp.DataModels.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterContext twitterContext;

        public UnitOfWork(TwitterContext twitterContext)
        {
            this.twitterContext = twitterContext;
        }

        public int SaveChanges()
        {
            return this.twitterContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.twitterContext.SaveChangesAsync();
        }
    }
}