using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface IReadonlyRepository<TEntity, TKey> where TEntity : class, IIdentifiable<TKey>
    {
        TEntity GetById(TKey key);
        ICollection<TEntity> All { get; }
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}