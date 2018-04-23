using System.Collections.Generic;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface ITweetRepository : IGenericRepository<Tweet>
    {
        IEnumerable<Tweet> GetTweetsByUserId(string id);
    }
}