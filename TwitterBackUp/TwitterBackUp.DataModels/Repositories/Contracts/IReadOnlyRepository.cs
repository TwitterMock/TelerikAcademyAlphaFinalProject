using System.Collections.Generic;
using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface IReadonlyRepository<out TEntity> where TEntity : class, IDomainModel
    {
        IEnumerable<TEntity> All { get; }
        TEntity GetById(params object[] keyValues);
    }
}