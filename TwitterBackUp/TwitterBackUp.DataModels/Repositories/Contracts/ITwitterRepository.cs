using System.Collections.Generic;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface ITwitterRepository : IGenericRepository<Twitter>
    {
        IEnumerable<Twitter> GetTwittersByUserId(string id);
    }
}