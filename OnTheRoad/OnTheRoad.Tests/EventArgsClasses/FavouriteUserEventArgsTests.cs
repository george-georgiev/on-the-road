using System;
using NUnit.Framework;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class FavouriteUserEventArgsTests
    {
        [Test]
        public void WhenFavouriteUserEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new FavouriteUserEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<FavouriteUserEventArgs>());
        }

        [Test]
        public void VerifyThatFavouriteUserUsernameCanBeGettedSetted()
        {
            var addReviewEventArgs = new FavouriteUserEventArgs();
            var user = "user";

            addReviewEventArgs.FavouriteUserUsername = user;

            Assert.That(addReviewEventArgs.FavouriteUserUsername.Equals(user));
        }

        [Test]
        public void VerifyThatFavouriteUserUsernameReturnsInstanceOfString()
        {
            var addReviewEventArgs = new FavouriteUserEventArgs();
            var user = "user";

            addReviewEventArgs.FavouriteUserUsername = user;

            Assert.That(addReviewEventArgs.FavouriteUserUsername, Is.InstanceOf<string>());
        }

        [Test]
        public void VerifyThatCurrentUserUsernameCanBeGettedSetted()
        {
            var addReviewEventArgs = new FavouriteUserEventArgs();
            var user = "user";

            addReviewEventArgs.CurrentUserUsername = user;

            Assert.That(addReviewEventArgs.CurrentUserUsername.Equals(user));
        }

        [Test]
        public void VerifyThatCurrentUserUsernameReturnsInstanceOfString()
        {
            var addReviewEventArgs = new FavouriteUserEventArgs();
            var user = "user";

            addReviewEventArgs.CurrentUserUsername = user;

            Assert.That(addReviewEventArgs.CurrentUserUsername, Is.InstanceOf<string>());
        }
    }
}
