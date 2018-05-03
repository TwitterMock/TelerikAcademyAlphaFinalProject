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

namespace TwitterBackUp.ServicesTests.TwitterServicesTests
{
    [TestFixture]
    class SaveTwitterByUserId_Should
    {
        [Test]
        public void ThrowArgumentNullEx_When_UserId_IsNull ()
        {
            var unitMock = new Mock<IUnitOfWork>();
            var twitterRepoMock = new Mock<ITwitterRepository>();

            var twitterService = new TwitterService(unitMock.Object, twitterRepoMock.Object);
            string userId = null;
            var twitter = new Twitter { Id = "1284738912734897" };

            Assert.Throws<ArgumentNullException>(()=>twitterService.SaveTwitterByUserId(userId,twitter));
        }
        [Test]
        public void ThrowArgumentEx_When_UserId_IsEmptyString()
        {
            var unitMock = new Mock<IUnitOfWork>();
            var twitterRepoMock = new Mock<ITwitterRepository>();

            var twitterService = new TwitterService(unitMock.Object, twitterRepoMock.Object);
            string userId = String.Empty;
            var twitter = new Twitter { Id = "1284738912734897" };

            Assert.Throws<ArgumentException>(() => twitterService.SaveTwitterByUserId(userId, twitter));
        }
        [Test]
        public void ThrowArgumentNullEx_When_Twitter_IsNull()
        {
            var unitMock = new Mock<IUnitOfWork>();
            var twitterRepoMock = new Mock<ITwitterRepository>();

            var twitterService = new TwitterService(unitMock.Object, twitterRepoMock.Object);
            string userId = "12";
            Twitter twitter = null;

            Assert.Throws<ArgumentNullException>(() => twitterService.SaveTwitterByUserId(userId, twitter));
        }
        [Test]
        public void Call_TwitterRepoSaveChanges_Once()
        {
            var twitter = new Twitter { Id = "123124125122131" };
            Twitter nullTwitter = null; 
    

            var unitMock = new Mock<IUnitOfWork>();


            var twitterRepoMock = new Mock<ITwitterRepository>();
            var twitterService = new TwitterService(unitMock.Object, twitterRepoMock.Object);

            var userId = "12312";

            twitterRepoMock.Setup(x => x.GetById("123124125122131"))
                .Returns(nullTwitter);

            unitMock.Setup(x => x.SaveChanges()).Returns(It.IsAny<Int32>()).Verifiable();
            

         

            twitterService.SaveTwitterByUserId(userId, twitter);

            unitMock.Verify(t=>t.SaveChanges(),Times.Once);
        }
        [Test]
        public void Call_TwitterRepoInsert_Once()
        {
            var twitter = new Twitter { Id = "123124125122131" };
            Twitter nullTwitter = null;


            var unitMock = new Mock<IUnitOfWork>();


            var twitterRepoMock = new Mock<ITwitterRepository>();
            var twitterService = new TwitterService(unitMock.Object, twitterRepoMock.Object);

            var userId = "12312";

            twitterRepoMock.Setup(x => x.GetById("123124125122131"))
                .Returns(nullTwitter);

            twitterRepoMock.Setup(x => x.Insert(twitter)).Verifiable();




            twitterService.SaveTwitterByUserId(userId, twitter);

            twitterRepoMock.Verify(t => t.Insert(twitter), Times.Once);
        }
        [Test]
        public void DoesNotCall_TwitterRepoInsert_WhenTheTwitterIsInTheDb()
        {
            var twitter = new Twitter { Id = "123124125122131" };
         


            var unitMock = new Mock<IUnitOfWork>();


            var twitterRepoMock = new Mock<ITwitterRepository>();
            var twitterService = new TwitterService(unitMock.Object, twitterRepoMock.Object);

            var userId = "12312";

            twitterRepoMock.Setup(x => x.GetById("123124125122131"))
                .Returns(twitter);

            twitterRepoMock.Setup(x => x.Insert(twitter)).Verifiable();




            twitterService.SaveTwitterByUserId(userId, twitter);

            twitterRepoMock.Verify(t => t.Insert(twitter),Times.Never);
        }


    }
}
