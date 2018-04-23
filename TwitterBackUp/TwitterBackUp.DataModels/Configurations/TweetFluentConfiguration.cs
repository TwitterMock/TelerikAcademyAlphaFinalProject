using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Configurations.Contracts;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Configurations
{
    public class TweetFluentConfiguration : IFluentConfiguration
    {
        public void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweet>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Tweet>()
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}