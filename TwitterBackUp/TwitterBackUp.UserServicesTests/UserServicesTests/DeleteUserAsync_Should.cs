using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.Services.Services;
using TwitterBackUp.Services.Utils;

namespace TwitterBackUp.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class DeleteUserAsync_Should
    {
        [Test]
        public async Task Throw_NullEx_When_UserId_Is_Null()
        {
            var userManagerMock = new Mock<IUserManagerProvider>();
            var tweetRepoMock = new Mock<ITweetRepository>();
            var userService = new UserServices(tweetRepoMock.Object,userManagerMock.Object);
           Assert.ThrowsAsync<ArgumentNullException>( async () => await userService.DeleteUserAsync(null));
        }
    }
}
