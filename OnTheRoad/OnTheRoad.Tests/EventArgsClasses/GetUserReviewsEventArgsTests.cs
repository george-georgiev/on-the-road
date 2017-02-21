using System;
using NUnit.Framework;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class GetUserReviewsEventArgsTests
    {
        [Test]
        public void WhenGetUserReviewsEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new GetUserReviewsEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<GetUserReviewsEventArgs>());
        }

        [Test]
        public void VerifyThatUsernameCanBeGettedSetted()
        {
            var getUserReviewsEventArgs = new GetUserReviewsEventArgs();
            var username = "user";

            getUserReviewsEventArgs.Username = username;

            Assert.That(getUserReviewsEventArgs.Username.Equals(username));
        }

        [Test]
        public void VerifyThatUsernameReturnsInstanceOfString()
        {
            var getUserReviewsEventArgs = new GetUserReviewsEventArgs();
            var username = "user";

            getUserReviewsEventArgs.Username = username;

            Assert.That(getUserReviewsEventArgs.Username, Is.InstanceOf<string>());
        }
    }
}
