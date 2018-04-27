using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Tests.Repositories.GenericRepositoryTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void InvokeFind_WhenPassedEntityPrimaryKey()
        {
            var tweetId = "12893478932745";

            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();

            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            tweetsDbSetMock.Setup(x => x.Find(It.IsAny<string>()))
                .Verifiable();

            var tweetRepository = new GenericRepository<Tweet>(contextMock.Object);
            tweetRepository.GetById(tweetId);

            tweetsDbSetMock.Verify(s => s.Find(It.IsAny<string>()), Times.Once);
        }
    }
}