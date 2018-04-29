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
                .HasKey(x => x.Id);

            modelBuilder.Entity<Twitter>()
                .Property(x => x.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Twitter>()
                .HasAlternateKey(c => c.ScreenName)
                .HasName("AlternateKey_ScreenName");
        }
    }
}