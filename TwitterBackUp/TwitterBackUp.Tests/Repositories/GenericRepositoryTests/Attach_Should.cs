using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Tests.Repositories.GenericRepositoryTests
{
    [TestFixture]
    public class Attach_Should
    {
        [Test]
        public void ThrowArgumentNullException_When_PassedNull()
        {
            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();

            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            var tweetRepository = new GenericRepository<Tweet>(contextMock.Object);

            Assert.That(() => tweetRepository.Attach(null), Throws.ArgumentNullException);
        }

        [Test]
        public void InvokeAttach_WhenPassedEntity()
        {
            var tweet = new Tweet { Id = "1284738912734897" };
            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();

            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            tweetsDbSetMock.Setup(x => x.Attach(It.IsAny<Tweet>()))
                .Verifiable();

            var tweetRepository = new GenericRepository<Tweet>(contextMock.Object);
            tweetRepository.Attach(tweet);

            tweetsDbSetMock.Verify(s => s.Attach(It.IsAny<Tweet>()), Times.Once);
        }
    }
}