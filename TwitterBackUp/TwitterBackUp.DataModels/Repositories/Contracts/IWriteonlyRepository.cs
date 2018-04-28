using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface IWriteonlyRepository<TEntity, TKey> where TEntity : class, IIdentifiable<TKey>
    {
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Attach(TEntity entity);
        void Update(TEntity entity);
    }
}