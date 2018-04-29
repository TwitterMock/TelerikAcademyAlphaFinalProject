using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface IGenericRepository<TEntity, TKey> : IReadonlyRepository<TEntity, TKey>, IWriteonlyRepository<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {
    }
}