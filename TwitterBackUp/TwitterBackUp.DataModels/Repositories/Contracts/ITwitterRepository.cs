using System.Collections.Generic;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface ITwitterRepository : IGenericRepository<Twitter, string>
    {
        ICollection<Twitter> GetManyByUserId(string id);
        Twitter GetSingle(string screenName, string userId);
        int DeleteSingleTwitter(string twitterId, string userId);
    }
}