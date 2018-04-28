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
        public class GenericRepository<TEntity> : IGenericRepository<TEntity>
            where TEntity : class, IDomainModel
        {
            public GenericRepository(TwitterContext context)
            {
                this.Context = context;
                this.DbSet = this.Context.Set<TEntity>();
            }

            public void Delete(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException();
                }

                this.DbSet.Remove(entity);
            }

            public void Attach(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException();
                }

                this.DbSet.Attach(entity);
            }

            public TEntity GetById(params object[] keys)
            {
                return this.DbSet.Find(keys);
            }

            public IEnumerable<TEntity> All => this.DbSet;

            protected TwitterContext Context { get; }

            protected DbSet<TEntity> DbSet { get; }

            public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
            {
                return this.DbSet
                    .Where(predicate);
            }

            public void Insert(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException();
                }

                this.DbSet.Add(entity);
            }

            public void Update(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException();
                }

                this.DbSet.Update(entity);
            }
        }
    }
}