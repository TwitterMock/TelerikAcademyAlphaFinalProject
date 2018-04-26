using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.DataTests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private TwitterContext context;
        private IGenericRepository<Tweet> tweetRepository;

        private DbContextOptions options = new DbContextOptionsBuilder<TwitterContext>()
            .UseInMemoryDatabase(databaseName: "TwitterBackUpTesetDb")
            .Options;

        [OneTimeSetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestMethod1()
        {
        }
    }
}
