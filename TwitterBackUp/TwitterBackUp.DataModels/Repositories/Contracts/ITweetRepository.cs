using System.Collections.Generic;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface ITweetRepository : IGenericRepository<Tweet, string>
    {
        ICollection<Tweet> GetAllByUserId(string id);
        Tweet GetSingle(string tweetId, string userId);
        int DeleteSingle(string tweetId, string userId);
        int DeleteAllTweetsByUserId(string userId);
    }
}