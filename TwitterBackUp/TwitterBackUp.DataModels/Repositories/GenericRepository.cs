using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories
{
    namespace GetHired.DataModels.Repositories.Models
    {
        public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
            where TEntity : class, IIdentifiable<TKey>
        {
            public GenericRepository(TwitterContext context)
            {
                this.Context = context;
                this.DbSet = this.Context.Set<TEntity>();
            }

            public void Delete(TEntity entity)
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                
                this.DbSet.Remove(entity);
            }

            public void Attach(TEntity entity)
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                
                this.DbSet.Attach(entity);
            }

            public TEntity GetById(TKey key)
            {
                return this.DbSet.Find(key);
            }

            public ICollection<TEntity> All => this.DbSet.ToList();

            protected TwitterContext Context { get; }

            protected DbSet<TEntity> DbSet { get; }

            public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            {
                return this.DbSet
                    .Where(predicate).ToList();
            }

            public void Insert(TEntity entity)
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                
                this.DbSet.Add(entity);
            }

            public void Update(TEntity entity)
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                
                this.DbSet.Update(entity);
            }
        }
    }
}