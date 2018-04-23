using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Configurations.Contracts;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Configurations
{
    public class TwitterFluentConfiguration : IFluentConfiguration
    {
        public void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Twitter>()
                .HasMany(x => x.Tweets)
                .WithOne(x => x.Twitter)
                .HasForeignKey(x => x.TwitterId);

            modelBuilder.Entity<Twitter>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Twitter>()
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}