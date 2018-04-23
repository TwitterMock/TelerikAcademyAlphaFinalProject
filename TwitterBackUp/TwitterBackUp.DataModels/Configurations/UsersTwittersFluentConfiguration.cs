using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Configurations.Contracts;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Configurations
{
    public class UsersTwittersFluentConfiguration : IFluentConfiguration
    {
        public void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersTwitters>()
                .HasKey(x => new {x.UserId, x.TwitterId});

            modelBuilder.Entity<UsersTwitters>()
                .HasOne(x => x.Twitter)
                .WithMany(x => x.UsersTwitters)
                .HasForeignKey(x => x.TwitterId);
        }
    }
}