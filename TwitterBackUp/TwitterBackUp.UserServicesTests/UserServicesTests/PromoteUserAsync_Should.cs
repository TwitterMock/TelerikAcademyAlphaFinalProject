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

namespace TwitterBackUp.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class PromoteUserAsync_Should
    {
        [Test]
        public async Task Call_FindByIdAsync_Once()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            var user = new ApplicationUser { Id = "1231412" };
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            var userId = "1231412";

            await userService.PromoteUserAsync(userId);
            userManagerMock.Verify(x => x.FindByIdAsync(userId), Times.Once);
        }
        [Test]
        public void Throw_NullEx_If_FindByIdAsync_Return_Null()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            ApplicationUser user = null;
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            var userId = "1231412";

         
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.PromoteUserAsync(userId));
        }
        [Test]
        public void Throw_NullEx_If_UserId_Is_Null()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
 
            Assert.ThrowsAsync<ArgumentException>(async () => await userService.PromoteUserAsync(null));
        }
        [Test]
        public async Task Call_AddToRoleAsync_Once()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object, userManagerMock.Object);
            var user = new ApplicationUser { Id = "1231412" };
            var userId = "1231412";
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();
            userManagerMock.Setup(x => x.AddToRoleAsync(user, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            await userService.PromoteUserAsync(userId);
            userManagerMock.Verify(x => x.AddToRoleAsync(user,It.IsAny<string>()), Times.Once);
        }

    }
}
