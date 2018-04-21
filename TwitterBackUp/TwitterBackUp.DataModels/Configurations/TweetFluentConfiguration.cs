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
                .HasMany(x => x.TwitteTweets)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            modelBuilder.Entity<Tweet>()
                .HasKey(x => x.TweetId);

            modelBuilder.Entity<Tweet>()
                .Property(x => x.TweetId)
                .ValueGeneratedNever();
        }
    }
}