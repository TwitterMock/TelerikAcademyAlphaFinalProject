using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.Services.Services;
using TwitterBackUp.Services.Utils;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class DeleteUserAsync_Should
    {
        [Test]
        public void Throw_NullEx_When_UserId_Is_Null()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.DeleteUserAsync(null));
        }

        [Test]
        public async Task IsFindById_Called_Once()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            var user = new ApplicationUser { Id = "1231412" };
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            var userId = "1231412";

            await userService.DeleteUserAsync(userId);
            userManagerMock.Verify(x => x.FindByIdAsync(userId), Times.Once);

        }
        [Test]
        public async Task Call_DeleteAllTweetsByUserId_Once()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);

            tweetRepoMock.Setup(x => x.DeleteAllTweetsByUserId(It.IsAny<string>())).Verifiable();
            var userId = "1231412";
            await userService.DeleteUserAsync(userId);
            tweetRepoMock.Verify(x => x.DeleteAllTweetsByUserId(userId), Times.Once);


        }
        [Test]
        public async Task Call_DeleteUserAsync_Once()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            var userId = "1231412";
            var user = new ApplicationUser { Id = userId };

            userManagerMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);

            userManagerMock.Setup(x => x.DeleteUserAsync(user)).ReturnsAsync(IdentityResult.Success). Verifiable();


            await userService.DeleteUserAsync(userId);

            userManagerMock.Verify(x => x.DeleteUserAsync(user), Times.Once);

        }
        [Test]
        public async Task Return_Deleted_When_Executed()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            var user = new ApplicationUser { Id = "1231412" };
            var userId = "1231412";

            var result = await userService.DeleteUserAsync(userId);

            Assert.AreEqual("deleted", result);

        }

    }
}
