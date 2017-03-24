using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Models;
using OnTheRoad.Logic.Services;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void UserService_WhenInitializedWithNullForIUserRepository_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, unitOfWorkMock.Object));
        }

        [Test]
        public void UserService_WhenInitializedWithNullForIUserRepository_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new UserService(null, unitOfWorkMock.Object));
            StringAssert.Contains("userRepository cannot be null!", exc.Message);
        }

        [Test]
        public void UserService_WhenInitializedWithNullForIUnitOfWork_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(userRepositoryMock.Object, null));
        }

        [Test]
        public void UserService_WhenInitializedWithNullForIUnitOfWork_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new UserService(userRepositoryMock.Object, null));
            StringAssert.Contains("unitOfWork cannot be null!", exc.Message);
        }

        [Test]
        public void GetUserInfo_WhenCalled_ShouldReturnInstanceOfIUser()
        {
            var iUserMock = new Mock<IUser>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = userService.GetUserInfo("username");

            Assert.That(actual, Is.InstanceOf<IUser>());
        }

        [Test]
        public void UserRepository_WhenGetUserInfoIsCalled_ShouldCallGetByUsernameExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = userService.GetUserInfo("username");

            userRepositoryMock.Verify(x => x.GetByUserName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenUpdateUserInfoIsCalled_ShouldCallGetByUsernameExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateUserInfo("username", "fName", "lName", "phoneNumber", "info", new byte[0], iCityMock.Object);

            userRepositoryMock.Verify(x => x.GetByUserName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenUpdateUserInfoIsCalled_ShouldCallUpdateExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateUserInfo("username", "fName", "lName", "phoneNumber", "info", new byte[0], iCityMock.Object);

            userRepositoryMock.Verify(x => x.Update(It.IsAny<IUser>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenUpdateUserInfoIsCalled_ShouldCallUpdateWithTheCorrectUserExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateUserInfo("username", "fName", "lName", "phoneNumber", "info", new byte[0], iCityMock.Object);

            userRepositoryMock.Verify(x => x.Update(iUserMock.Object), Times.Once);
        }

        [Test]
        public void UnitOfWork_WhenUpdateUserInfoIsCalled_ShouldCallCommitExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateUserInfo("username", "fName", "lName", "phoneNumber", "info", new byte[0], iCityMock.Object);

            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [TestCase("fName", "lName", "phoneNumber", "info")]
        [TestCase("Sub", "Zero", "123321", "Will Freeze you!")]
        [Test]
        public void User_WhenUpdateUserInfoIsCalled_ShouldHaveTheCorrectPropertiesSet(string fName, string lName, string phoneNumber, string info)
        {
            var userMock = new User();
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(userMock);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateUserInfo("username", fName, lName, phoneNumber, info, new byte[0], iCityMock.Object);

            Assert.That(userMock.FirstName.Equals(fName));
            Assert.That(userMock.LastName.Equals(lName));
            Assert.That(userMock.Info.Equals(info));
            Assert.That(userMock.PhoneNumber.Equals(phoneNumber));
            Assert.That(userMock.City.Equals(iCityMock.Object));
        }

        [Test]
        public void UserRepository_WhenRemoveFavouriteUserIsCalled_ShouldCallGetByUsernameExactlyOnce()
        {
            var userToRemove = "Sub-Zero";
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.Username).Returns(userToRemove);
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>() { iUserMock.Object});
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.RemoveFavouriteUser("username", userToRemove);

            userRepositoryMock.Verify(x => x.GetByUserName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenRemoveFavouriteUserIsCalled_ShouldCallUpdateExactlyOnce()
        {
            var userToRemove = "Sub-Zero";
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.Username).Returns(userToRemove);
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>() { iUserMock.Object });
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.RemoveFavouriteUser("username", userToRemove);

            userRepositoryMock.Verify(x => x.Update(It.IsAny<IUser>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenRemoveFavouriteUserIsCalled_ShouldCallUpdateWithTheCorrectUserExactlyOnce()
        {
            var userToRemove = "Sub-Zero";
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.Username).Returns(userToRemove);
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>() { iUserMock.Object });
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.RemoveFavouriteUser("username", userToRemove);

            userRepositoryMock.Verify(x => x.Update(iUserMock.Object), Times.Once);
        }

        [Test]
        public void UnitOfWork_WhenRemoveFavouriteUserIsCalled_ShouldCallCommitExactlyOnce()
        {
            var userToRemove = "Sub-Zero";
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.Username).Returns(userToRemove);
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>() { iUserMock.Object });
            var iCityMock = new Mock<ICity>();
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.RemoveFavouriteUser("username", userToRemove);

            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserRepository_WhenAddFavouriteUserIsCalled_ShouldCallGetByUsernameTwice()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.AddFavouriteUser("username", "userToAddUsername");

            userRepositoryMock.Verify(x => x.GetByUserName(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void UserRepository_WhenAddFavouriteUserIsCalled_ShouldCallUpdateExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.AddFavouriteUser("username", "userToAddUsername");

            userRepositoryMock.Verify(x => x.Update(It.IsAny<IUser>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenAddFavouriteUserIsCalled_ShouldCallUpdateWithTheCorrectUserExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.AddFavouriteUser("username", "userToAddUsername");

            userRepositoryMock.Verify(x => x.Update(iUserMock.Object), Times.Once);
        }

        [Test]
        public void UnitOfWork_WhenAddFavouriteUserIsCalled_ShouldCallCommitExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.AddFavouriteUser("username", "userToAddUsername");

            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserRepository_WhenUpdateImageIsCalled_ShouldCallGetByUsernameOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateImage(new byte[3], "username");

            userRepositoryMock.Verify(x => x.GetByUserName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenUpdateImageIsCalled_ShouldCallUpdateExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateImage(new byte[3], "username");

            userRepositoryMock.Verify(x => x.Update(It.IsAny<IUser>()), Times.Once);
        }

        [Test]
        public void UserRepository_WhenUpdateImageIsCalled_ShouldCallUpdateWithTheCorrectUserExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateImage(new byte[3], "username");

            userRepositoryMock.Verify(x => x.Update(iUserMock.Object), Times.Once);
        }

        [Test]
        public void UnitOfWork_WhenUpdateImageIsCalled_ShouldCallCommitExactlyOnce()
        {
            var iUserMock = new Mock<IUser>();
            iUserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>());
            this.userRepositoryMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(iUserMock.Object);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.UpdateImage(new byte[3], "username");

            userRepositoryMock.Verify(x => x.Update(iUserMock.Object), Times.Once);

            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserRepository_WhenGetAllUsersCountIsCalle_ShouldCallGetAllUsersCountExactlyOnce()
        {
            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
            userService.GetAllUsersCount();

            userRepositoryMock.Verify(x => x.GetAllUsersCount(), Times.Once);
        }
    }
}
