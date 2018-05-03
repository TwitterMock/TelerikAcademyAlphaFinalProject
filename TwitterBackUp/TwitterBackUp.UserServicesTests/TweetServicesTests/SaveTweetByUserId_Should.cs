using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.DataModels.Contracts;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.DataModels.Repositories;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DomainModels;
using TwitterBackUp.Services.Services;
using TwitterBackUp.Services.Services.Contracts;

namespace TwitterBackUp.ServicesTests.TweetServicesTests
{
    [TestFixture]
    class SaveTweetByUserId_Should
    {
        [Test]
        public void ThrowArgumentNullEx_When_UserId_IsNull()
        {
            var unitMock = new Mock<IUnitOfWork>();
            var tweetRepoMock = new Mock<ITweetRepository>();

            var tweetService = new TweetService(unitMock.Object, tweetRepoMock.Object);
            string userId = null;
            var tweet = new Tweet { Id = "1284738912734897" };

            Assert.Throws<ArgumentNullException>(() => tweetService.SaveTweetByUserId(userId, tweet));
        }
        [Test]
        public void ThrowArgumentEx_When_UserId_IsEmptyString()
        {
            var unitMock = new Mock<IUnitOfWork>();
            var tweetRepoMock = new Mock<ITweetRepository>();

            var tweetService = new TweetService(unitMock.Object, tweetRepoMock.Object);
            string userId = String.Empty;
            var tweet = new Tweet { Id = "1284738912734897" };

            Assert.Throws<ArgumentException>(() => tweetService.SaveTweetByUserId(userId, tweet));
        }
        [Test]
        public void ThrowArgumentNullEx_When_Twitter_IsNull()
        {
            var unitMock = new Mock<IUnitOfWork>();
            var tweetRepoMock = new Mock<ITweetRepository>();

            var tweetService = new TweetService(unitMock.Object, tweetRepoMock.Object);
            string userId = "12";
            Tweet tweet = null;

            Assert.Throws<ArgumentNullException>(() => tweetService.SaveTweetByUserId(userId, tweet));
        }
        [Test]
        public void Call_TwitterRepoSaveChanges_Once()
        {
            var tweet = new Tweet { Id = "123124125122131" };
            Tweet nullTweet = null;


            var unitMock = new Mock<IUnitOfWork>();

            var tweetRepoMock = new Mock<ITweetRepository>();
            var tweetService = new TweetService(unitMock.Object, tweetRepoMock.Object);

            var userId = "12312";

            tweetRepoMock.Setup(x => x.GetById("123124125122131"))
                .Returns(nullTweet);

            unitMock.Setup(x => x.SaveChanges()).Returns(It.IsAny<Int32>()).Verifiable();

            tweetService.SaveTweetByUserId(userId, tweet);

            unitMock.Verify(t => t.SaveChanges(), Times.Once);
        }
    }
     
}
