using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TwitterBackUp.DataModels.Configurations.Contracts;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataModels.Models
{
    public class TwitterContext : DbContext
    {
        public TwitterContext()
        {
            
        }

        public TwitterContext(DbContextOptions<TwitterContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Twitter> Twitters { get; set; }
        public DbSet<UsersTweets> UsersTweets { get; set; }
        public DbSet<UsersTwitters> UsersTwitters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.RegisterConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void RegisterConfigurations(ModelBuilder modelBuilder)
        {
            var type = typeof(IFluentConfiguration);

            var configs = Assembly.GetAssembly(type)
                .GetTypes()
                .Where(x => type.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);

            foreach (var config in configs)
            {
                var instance = Activator.CreateInstance(config);
                (instance as IFluentConfiguration)?.Register(modelBuilder);
            }
        }
    }
}
