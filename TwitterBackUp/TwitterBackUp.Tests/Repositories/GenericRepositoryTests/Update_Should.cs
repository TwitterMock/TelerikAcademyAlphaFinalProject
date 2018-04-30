using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Tests.Repositories.GenericRepositoryTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void ThrowsArgumentNullException_When_PassedNull()
        {
            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();

            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            var tweetRepository = new GenericRepository<Tweet, string>(contextMock.Object);

            Assert.That(() => tweetRepository.Update(null), Throws.ArgumentNullException);
        }

        [Test]
        public void InvokeUpdate_WhenPassedEntity()
        {
            var tweet = new Tweet { Id = "1284738912734897" };
            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();

            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            tweetsDbSetMock.Setup(x => x.Update(It.IsAny<Tweet>()))
                .Verifiable();

            var tweetRepository = new GenericRepository<Tweet, string>(contextMock.Object);
            tweetRepository.Update(tweet);

            tweetsDbSetMock.Verify(s => s.Update(It.IsAny<Tweet>()), Times.Once);
        }
    }
}