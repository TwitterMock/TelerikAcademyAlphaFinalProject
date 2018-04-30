using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataTests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private TwitterContext context;
        private DbContextOptions<TwitterContext> options;
        
        [Test]
        public void Insert_ShouldAddEntityInDb()
        {
            this.options = new DbContextOptionsBuilder<TwitterContext>()
                .UseInMemoryDatabase(databaseName: "Insert_ShouldAddEntityInDb")
                .Options;

            var tweetId = "12345671273482342";

            using (this.context = new TwitterContext(this.options))
            {
                var tweetRepository = new GenericRepository<Tweet, string>(this.context);

                tweetRepository.Insert(new Tweet
                {
                    Id = tweetId,
                    Content = "some_content",
                    CreatedOn = DateTime.Now,
                    TwitterId = "1234892374972234",
                    TwitterScreenName = "Trump",
                });

                this.context.SaveChanges();
            }

            using (this.context = new TwitterContext(this.options))
            {
                var expected = this.context.Tweets.Find(tweetId).Id;
                Assert.That(tweetId, Is.EqualTo(expected));
            }
        }

        [Test]
        public void GetById_ShouldFindEntityByPrimaryKey()
        {
            this.options = new DbContextOptionsBuilder<TwitterContext>()
                .UseInMemoryDatabase(databaseName: "GetById_ShouldFindEntityByPrimaryKey")
                .Options;

            var tweetId = "12349891273482342";

            using (this.context = new TwitterContext(this.options))
            {
                this.context.Tweets.Add(new Tweet
                {
                    Id = tweetId,
                    Content = "some_content",
                    CreatedOn = DateTime.Now,
                    TwitterId = "1234892374972234",
                    TwitterScreenName = "Trump",
                });

                this.context.SaveChanges();
            }

            using (this.context = new TwitterContext(this.options))
            {
                var tweetRepository = new GenericRepository<Tweet, string>(this.context);
                var entity = tweetRepository.GetById(tweetId);

                Assert.That(entity, Is.Not.Null);
            }
        }
        
        [Test]
        public void Delete_ShouldRemoveEntityFromDb()
        {
            this.options = new DbContextOptionsBuilder<TwitterContext>()
                .UseInMemoryDatabase(databaseName: "Delete_ShouldRemoveEntityFromDb")
                .Options;

            var tweetId = "12349891273482342";

            var tweet = new Tweet
            {
                Id = tweetId,
                Content = "some_content",
                CreatedOn = DateTime.Now,
                TwitterId = "1234892374972234",
                TwitterScreenName = "Trump",
            };

            using (this.context = new TwitterContext(this.options))
            {
                this.context.Tweets.Add(tweet);
                this.context.SaveChanges();

                var tweetRepository = new GenericRepository<Tweet, string>(this.context);

                tweetRepository.Delete(tweet);
                this.context.SaveChanges();
            }

            using (this.context = new TwitterContext(this.options))
            {
                var actual = this.context.Tweets.Find(tweetId);
                Assert.That(actual, Is.Null);
            }
        }

        [Test]
        public void Update_ShouldUpdateEntityInDb()
        {
            this.options = new DbContextOptionsBuilder<TwitterContext>()
                .UseInMemoryDatabase(databaseName: "Update_ShouldUpdateEntityInDb")
                .Options;

            var tweetId = "12349891273482342";

            var tweet = new Tweet
            {
                Id = tweetId,
                Content = "some_content",
                CreatedOn = DateTime.Now,
                TwitterId = "1234892374972234",
                TwitterScreenName = "Trump",
            };

            using (this.context = new TwitterContext(this.options))
            {
                this.context.Tweets.Add(tweet);
                this.context.SaveChanges();
            }

            var expected = "BradPitt";
            tweet.TwitterScreenName = expected;

            using (this.context = new TwitterContext(this.options))
            {
                var tweetRepository = new GenericRepository<Tweet, string>(context);
                tweetRepository.Update(tweet);
                context.SaveChanges();
            }

            using (this.context = new TwitterContext(this.options))
            {
                var actual = this.context.Tweets.Find(tweetId).TwitterScreenName;
                Assert.That(actual, Is.EqualTo(expected));
            }
        }
    }
}
