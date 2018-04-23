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
                this.Context.Entry(entity).State = EntityState.Deleted;
            }

            public void Delete(object id)
            {
                var entity = this.GetById(id);

                if (entity != null)
                {
                    this.Delete(entity);
                }
            }

            public void Attach(TEntity entity)
            {
                this.Context.Entry(entity).State = EntityState.Unchanged;
            }

            public TEntity GetById(object id)
            {
                return this.DbSet.Find(id);
            }

            public IEnumerable<TEntity> All
            {
                get
                {
                    return this.DbSet
                        .AsEnumerable();
                }
            }

            protected TwitterContext Context { get; }

            protected DbSet<TEntity> DbSet { get; }

            public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate)
            {
                return this.DbSet
                    .Where(predicate)
                    .AsEnumerable();
            }

            public void Insert(TEntity entity)
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }

            public void Update(TEntity entity)
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }

            public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
            {
                return this.DbSet
                    .Where(predicate)
                    .AsEnumerable();
            }

        }
    }
}