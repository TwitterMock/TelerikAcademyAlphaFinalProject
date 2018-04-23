using System.Threading.Tasks;

namespace TwitterBackUp.DataModels.Contracts
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}