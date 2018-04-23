using Microsoft.EntityFrameworkCore;

namespace TwitterBackUp.DataModels.Configurations.Contracts
{
    public interface IFluentConfiguration
    {
        void Register(ModelBuilder modelBuilder);
    }
}