using System.Collections.Generic;
using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class ProfileInfoEventArgsTests
    {
        [Test]
        public void WhenProfileInfoEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new ProfileInfoEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<ProfileInfoEventArgs>());
        }

        [Test]
        public void VerifyThatUsernameCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var username = "SomeRandomUsername";

            profileInfoEventArgs.Username = username;

            Assert.That(profileInfoEventArgs.Username.Equals(username));
        }

        [Test]
        public void VerifyThatFirstNameCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var name = "SomeRandomname";

            profileInfoEventArgs.FirstName = name;

            Assert.That(profileInfoEventArgs.FirstName.Equals(name));
        }

        [Test]
        public void VerifyThatLastNameCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var name = "SomeRandomname";

            profileInfoEventArgs.LastName= name;

            Assert.That(profileInfoEventArgs.LastName.Equals(name));
        }

        [Test]
        public void VerifyThatInfoCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var info = "info";

            profileInfoEventArgs.Info = info;

            Assert.That(profileInfoEventArgs.Info.Equals(info));
        }

        [Test]
        public void VerifyThatEmailCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var email = "email";

            profileInfoEventArgs.Email= email;

            Assert.That(profileInfoEventArgs.Email.Equals(email));
        }

        [Test]
        public void VerifyThatCityIdCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var cityId = 12;

            profileInfoEventArgs.CityId= cityId;

            Assert.That(profileInfoEventArgs.CityId.Equals(cityId));
        }

        [Test]
        public void VerifyThatPhoneNumberCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var phoneNumber = "number";

            profileInfoEventArgs.PhoneNumber= phoneNumber;

            Assert.That(profileInfoEventArgs.PhoneNumber.Equals(phoneNumber));
        }

        [Test]
        public void VerifyThatFavouriteUsersCanBeGettedSetted()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var favouriteUsers = new List<IUser>();

            profileInfoEventArgs.FavouriteUsers = favouriteUsers;

            Assert.That(profileInfoEventArgs.FavouriteUsers.Equals(favouriteUsers));
        }

        [Test]
        public void VerifyThatFavouriteUsersReturnsICollectionFromIUser()
        {
            var profileInfoEventArgs = new ProfileInfoEventArgs();
            var favouriteUsers = new List<IUser>();

            profileInfoEventArgs.FavouriteUsers = favouriteUsers;

            Assert.That(profileInfoEventArgs.FavouriteUsers, Is.InstanceOf<ICollection<IUser>>());
        }
    }
}