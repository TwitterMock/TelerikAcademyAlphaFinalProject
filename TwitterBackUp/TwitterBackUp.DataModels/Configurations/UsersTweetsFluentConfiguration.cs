using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Configurations.Contracts;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Configurations
{
    public class UsersTweetsFluentConfiguration : IFluentConfiguration
    {
        public void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersTweets>()
                .HasKey(x => new { x.UserId, x.TweetId });

            modelBuilder.Entity<UsersTweets>()
                .HasOne(x => x.Tweet)
                .WithMany(x => x.UsersTweets)
                .HasForeignKey(x => x.TweetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersTweets>()
                .HasIndex(x => x.TweetId);

            modelBuilder.Entity<UsersTweets>()
                .HasIndex(x => x.UserId);
        }
    }
}