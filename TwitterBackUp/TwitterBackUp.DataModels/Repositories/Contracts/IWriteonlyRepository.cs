using TwitterBackUp.DomainModels.Contracts;

namespace TwitterBackUp.DataModels.Repositories.Contracts
{
    public interface IWriteonlyRepository<in TEntity> where TEntity : class, IDomainModel
    {
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        void Attach(TEntity entity);
        void Update(TEntity entity);
    }
}