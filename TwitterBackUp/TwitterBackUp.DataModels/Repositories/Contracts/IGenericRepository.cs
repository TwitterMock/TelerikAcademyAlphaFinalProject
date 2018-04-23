using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> : IReadonlyRepository<TEntity>, IWriteonlyRepository<TEntity>
        where TEntity : class, IDomainModel
    {
        IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
    }
}