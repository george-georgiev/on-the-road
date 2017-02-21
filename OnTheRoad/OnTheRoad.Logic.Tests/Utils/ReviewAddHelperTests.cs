using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Utils;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Tests.Utils
{
    [TestFixture]
    class ReviewAddHelperTests
    {
        [Test]
        public void ReviewAddHelper_WhenInitializedWithNullForUserGetService_ShouldThrowNewArgumentNullException()
        {
            var ratingServiceMock = new Mock<IRatingService>();

            Assert.Throws<ArgumentNullException>(() => new ReviewAddHelper(null, ratingServiceMock.Object));
        }

        [Test]
        public void ReviewAddHelper_WhenInitializedWithNullForUserGetService_ShouldThrowNewArgumentNullExceptionWithProperException()
        {
            var ratingServiceMock = new Mock<IRatingService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewAddHelper(null, ratingServiceMock.Object));
            StringAssert.Contains("userService cannot be null!", exc.Message);
        }

        [Test]
        public void ReviewAddHelper_WhenInitializedWithNullForRatingService_ShouldThrowNewArgumentNullException()
        {
            var userServiceMock = new Mock<IUserGetService>();

            Assert.Throws<ArgumentNullException>(() => new ReviewAddHelper(userServiceMock.Object, null));
        }

        [Test]
        public void ReviewAddHelper_WhenInitializedWithNullForRatingService_ShouldThrowNewArgumentNullExceptionWithProperException()
        {
            var userServiceMock = new Mock<IUserGetService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewAddHelper(userServiceMock.Object, null));
            StringAssert.Contains("ratingService cannot be null!", exc.Message);
        }

        [Test]
        public void RatingService_WhenGetRatingByValueIsCalled_ShouldCallGetRatingByValueExactlyOnce()
        {
            var userServiceMock = new Mock<IUserGetService>();
            var ratingServiceMock = new Mock<IRatingService>();

            var helper = new ReviewAddHelper(userServiceMock.Object, ratingServiceMock.Object);
            helper.GetRatingByValue(RatingEnum.Negative);

            ratingServiceMock.Verify(x => x.GetRatingByValue(It.IsAny<RatingEnum>()), Times.Once);
        }

        [Test]
        public void RatingService_WhenGetRatingByValueIsCalled_ShouldCallGetRatingByValueWithThePassedValue()
        {
            var passedValue = RatingEnum.Negative;
            var userServiceMock = new Mock<IUserGetService>();
            var ratingServiceMock = new Mock<IRatingService>();

            var helper = new ReviewAddHelper(userServiceMock.Object, ratingServiceMock.Object);
            helper.GetRatingByValue(passedValue);

            ratingServiceMock.Verify(x => x.GetRatingByValue(passedValue), Times.Once);
        }

        [Test]
        public void UserService_WhenGetUserByUsernameIsCalled_ShouldCallGetUserInfoExactlyOnce()
        {
            var userServiceMock = new Mock<IUserGetService>();
            var ratingServiceMock = new Mock<IRatingService>();
            var helper = new ReviewAddHelper(userServiceMock.Object, ratingServiceMock.Object);
            helper.GetUserByUsername("someName");

            userServiceMock.Verify(x => x.GetUserInfo(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserService_WhenGetUserByUsernameIsCalled_ShouldCallGetUserInfoWithThePassedValue()
        {
            var passedValue = "someUser";
            var userServiceMock = new Mock<IUserGetService>();
            var ratingServiceMock = new Mock<IRatingService>();
            var helper = new ReviewAddHelper(userServiceMock.Object, ratingServiceMock.Object);
            helper.GetUserByUsername(passedValue);

            userServiceMock.Verify(x => x.GetUserInfo(passedValue), Times.Once);
        }
    }
}