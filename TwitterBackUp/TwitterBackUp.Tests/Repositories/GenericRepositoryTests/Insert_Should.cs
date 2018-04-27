using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Tests.Repositories.GenericRepositoryTests
{
    [TestFixture]
    public class Insert_Should
    {
        [Test]
        public void ThrowArgumentNullException_When_PassedNull()
        {
            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();

            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            var tweetRepository = new GenericRepository<Tweet>(contextMock.Object);

            Assert.That(() => tweetRepository.Insert(null), Throws.ArgumentNullException);
        }

        [Test]
        public void InvokeAdd_WhenPassedEntity()
        {
            var tweet = new Tweet { Id = "1284738912734897" };
            var contextMock = new Mock<TwitterContext>();
            var tweetsDbSetMock = new Mock<DbSet<Tweet>>();
            
            contextMock.Setup(x => x.Set<Tweet>())
                .Returns(tweetsDbSetMock.Object);

            tweetsDbSetMock.Setup(x => x.Add(It.IsAny<Tweet>()))
                .Verifiable();

            var tweetRepository = new GenericRepository<Tweet>(contextMock.Object);
            tweetRepository.Insert(tweet);

            tweetsDbSetMock.Verify(s => s.Add(It.IsAny<Tweet>()), Times.Once);
        }
    }
}