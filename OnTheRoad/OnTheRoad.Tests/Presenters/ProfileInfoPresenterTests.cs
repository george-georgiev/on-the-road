using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Mvp.Profile.Contracts;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Domain.Models;
using OnTheRoad.Mvp.Models;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class ProfileInfoPresenterTests
    {
        [Test]
        public void ProfileInfoPresenter_WhenInitializedWithNullForUserService_ShouldThrowNewArgumentExeption()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var cityServiceMock = new Mock<ICityService>();

            Assert.Throws<ArgumentNullException>(() => new ProfileInfoPresenter(profileInfoViewMock.Object, null, cityServiceMock.Object));
        }

        [Test]
        public void ProfileInfoPresenter_WhenInitializedWithNullForUserService_ShouldThrowProperExeptionMessage()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var cityServiceMock = new Mock<ICityService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ProfileInfoPresenter(profileInfoViewMock.Object, null, cityServiceMock.Object));
            StringAssert.Contains("userService cannot be null.", exc.Message);
        }

        [Test]
        public void ProfileInfoPresenter_WhenInitializedWithNullForCityService_ShouldThrowNewArgumentExeption()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();

            Assert.Throws<ArgumentNullException>(() => new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, null));
        }

        [Test]
        public void ProfileInfoPresenter_WhenInitializedWithNullForCityrService_ShouldThrowProperExeptionMessage()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, null));
            StringAssert.Contains("cityService cannot be null.", exc.Message);
        }

        [Test]
        public void UserService_WhenUpdateProfileImageIsRaise_ShouldCallUpdateImageExactlyOnce()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.UpdateProfileImage += null, null, new ProfileImageEventArgs());

            userServiceMock.Verify(x => x.UpdateImage(It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserService_WhenAddFavouriteUserIsRaise_ShouldCallAddFavouriteUserExactlyOnce()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.AddFavouriteUser += null, null, new FavouriteUserEventArgs());

            userServiceMock.Verify(x => x.AddFavouriteUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase("userOne", "UserTwo")]
        [TestCase("Sub-Zero", "Scorpion")]
        [Test]
        public void UserService_WhenAddFavouriteUserIsRaise_ShouldCallAddFavouriteUserWithCorrectParameters(string currentUser, string userToAdd)
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.AddFavouriteUser += null, null,
                 new FavouriteUserEventArgs()
                 {
                     CurrentUserUsername = currentUser,
                     FavouriteUserUsername = userToAdd
                 });

            userServiceMock.Verify(x => x.AddFavouriteUser(currentUser, userToAdd), Times.Once);
        }

        [Test]
        public void UserService_WhenRemoveFavouriteUserIsRaise_ShouldCallRemoveFavouriteUserExactlyOnce()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.RemoveFavouriteUser += null, null, new FavouriteUserEventArgs());

            userServiceMock.Verify(x => x.RemoveFavouriteUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase("userOne", "UserTwo")]
        [TestCase("Sub-Zero", "Scorpion")]
        [Test]
        public void UserService_WhenRemoveFavouriteUserIsRaise_ShouldCallRemoveFavouriteUserWithCorrectParameters(string currentUser, string userToRemove)
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.RemoveFavouriteUser += null, null,
                new FavouriteUserEventArgs()
                {
                    CurrentUserUsername = currentUser,
                    FavouriteUserUsername = userToRemove
                });

            userServiceMock.Verify(x => x.RemoveFavouriteUser(currentUser, userToRemove), Times.Once);
        }

        [Test]
        public void UserService_WhenUpdateProfileInfoIsRaise_ShouldCallUpdateUserInfoExactlyOnce()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.UpdateProfileInfo += null, null, new ProfileInfoEventArgs());

            userServiceMock.Verify(x => x.UpdateUserInfo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ICity>()), Times.Once);
        }

        [TestCase("username", "fName", "lName", "phoneNumber", "info")]
        [TestCase("Sub-Zero", "Sub", "Zero", "123321", "Freeeze")]
        [Test]
        public void UserService_WhenUpdateProfileInfoIsRaise_ShouldCallUpdateUserInfoWithCorrectParameters(string username, string fName, string lName, string phoneNumber, string info)
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var cityMock = new Mock<ICity>();
            cityServiceMock.Setup(x => x.GetCityById(It.IsAny<int>())).Returns(cityMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.UpdateProfileInfo += null, null,
                new ProfileInfoEventArgs()
                {
                    Username = username,
                    FirstName = fName,
                    LastName = lName,
                    PhoneNumber = phoneNumber,
                    Info = info
                });

            userServiceMock.Verify(x => x.UpdateUserInfo(username, fName, lName, phoneNumber, info, cityMock.Object), Times.Once);
        }

        [Test]
        public void CityService_WhenUpdateProfileInfoIsRaise_ShouldCallGetCityByIdExactlyOnce()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.UpdateProfileInfo += null, null, new ProfileInfoEventArgs());

            cityServiceMock.Verify(x => x.GetCityById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UserService_WhenGetProfileInfoIsRaise_ShouldCallGetUserInfoExactlyOnce()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs() { Username = "Sub-Zero" });

            userServiceMock.Verify(x => x.GetUserInfo(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectUsername()
        {
            var username = "Sub-Zero";
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.Username).Returns(username);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.Username.Equals(username));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectFirstName()
        {
            var firtName = "Sub-Zero";
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.FirstName).Returns(firtName);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.FirstName.Equals(firtName));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectLastName()
        {
            var lastName = "Sub-Zero";
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.LastName).Returns(lastName);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.LastName.Equals(lastName));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectPhoneNumber()
        {
            var phoneNumber = "11111";
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.PhoneNumber).Returns(phoneNumber);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.PhoneNumber.Equals(phoneNumber));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectEmail()
        {
            var email = "sub@zero";
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.Email).Returns(email);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.Email.Equals(email));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectInfo()
        {
            var info = "freeze";
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.Info).Returns(info);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.Info.Equals(info));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectImage()
        {
            var image = new byte[10];
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            userMock.SetupGet(x => x.Image).Returns(image);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.Image.Equals(image));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectCity()
        {
            var cityMock = new Mock<ICity>();
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            cityMock.SetupGet(x => x.Name).Returns("Varna");
            userMock.SetupGet(x => x.City).Returns(cityMock.Object);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.City.Equals(cityMock.Object.Name));
        }

        [Test]
        public void ViewModelFavouriteUsers_WhenGetProfileInfoIsRaise_ShouldSetCurrectFavouriteUsers()
        {
            var profileInfoViewMock = new Mock<IProfileInfoView>();
            var userServiceMock = new Mock<IUserService>();
            var cityServiceMock = new Mock<ICityService>();
            var profileInfoModelMock = new Mock<ProfileInfoModel>();
            var userMock = new Mock<IUser>();
            var favouriteUsers = new List<IUser>() { userMock.Object };
            userMock.SetupGet(x => x.FavouriteUsers).Returns(favouriteUsers);
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(userMock.Object);
            profileInfoViewMock.Setup(x => x.Model).Returns(profileInfoModelMock.Object);

            var presenter = new ProfileInfoPresenter(profileInfoViewMock.Object, userServiceMock.Object, cityServiceMock.Object);
            profileInfoViewMock.Raise(x => x.GetProfileInfo += null, null, new ProfileInfoEventArgs());

            Assert.That(profileInfoViewMock.Object.Model.FavouriteUsers.Equals(favouriteUsers));
        }
    }
}