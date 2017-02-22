using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Factories;
using OnTheRoad.Logic.Services;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private Mock<IReviewFactory> reviewFactoryMock;
        private Mock<IReviewDataUtils> reviewDataUtilsMock;
        private Mock<IReviewAddHelper> reviewAddHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.reviewFactoryMock = new Mock<IReviewFactory>();
            this.reviewDataUtilsMock = new Mock<IReviewDataUtils>();
            this.reviewAddHelperMock = new Mock<IReviewAddHelper>();
        }

        [Test]
        public void ReviewService_WhenInitializedWithNullForReviewFactory_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, null));
        }

        [Test]
        public void ReviewService_WhenInitializedWithNullForReviewFactory_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, null));
            StringAssert.Contains("reviewFactory cannot be null!", exc.Message);
        }

        [Test]
        public void ReviewService_WhenInitializedWithNullForReviewAddHelper_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ReviewService(null, reviewDataUtilsMock.Object, reviewFactoryMock.Object));
        }

        [Test]
        public void ReviewService_WhenInitializedWithNullForReviewAddHelper_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewService(null, reviewDataUtilsMock.Object, reviewFactoryMock.Object));
            StringAssert.Contains("reviewAddHelper cannot be null!", exc.Message);
        }

        [Test]
        public void ReviewService_WhenInitializedWithNullForReviewDataUtils_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ReviewService(reviewAddHelperMock.Object, null, reviewFactoryMock.Object));
        }

        [Test]
        public void ReviewService_WhenInitializedWithNullForReviewDataUtils_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewService(reviewAddHelperMock.Object, null, reviewFactoryMock.Object));
            StringAssert.Contains("reviewDataUtils cannot be null!", exc.Message);
        }

        [Test]
        public void ReviewAddHelper_WhenAddUserReviewIsCalled_ShouldCallGetRatingByValueExactlyOnce()
        {
            var reviewService = new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, reviewFactoryMock.Object);
            reviewService.AddUserReview("content", "fromUser", "toUser", RatingEnum.Positive, DateTime.Now);

            reviewAddHelperMock.Verify(x => x.GetRatingByValue(It.IsAny<RatingEnum>()), Times.Once);
        }

        [Test]
        public void ReviewAddHelper_WhenAddUserReviewIsCalled_ShouldCallGetUserByUsernameTwice()
        {
            var reviewService = new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, reviewFactoryMock.Object);
            reviewService.AddUserReview("content", "fromUser", "toUser", RatingEnum.Positive, DateTime.Now);

            reviewAddHelperMock.Verify(x => x.GetUserByUsername(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void ReviewFactory_WhenAddUserReviewIsCalled_ShouldCallCreateReviewExactlyOnce()
        {
            var reviewService = new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, reviewFactoryMock.Object);
            reviewService.AddUserReview("content", "fromUser", "toUser", RatingEnum.Positive, DateTime.Now);

            reviewFactoryMock.Verify(x => x.CreateReview(It.IsAny<string>(), It.IsAny<IUser>(), It.IsAny<IUser>(), It.IsAny<IRating>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void ReviewDatUtils_WhenAddUserReviewIsCalled_ShouldCallAddUserReviewExactlyOnce()
        {
            var reviewService = new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, reviewFactoryMock.Object);
            reviewService.AddUserReview("content", "fromUser", "toUser", RatingEnum.Positive, DateTime.Now);

            reviewDataUtilsMock.Verify(x => x.AddUserReview(It.IsAny<IReview>()), Times.Once);
        }

        [Test]
        public void ReviewDatUtils_WhenAddUserReviewIsCalled_ShouldCallGetUserReviewsExactlyOnce()
        {
            var reviewService = new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, reviewFactoryMock.Object);
            reviewService.GetUserReviews("username");

            reviewDataUtilsMock.Verify(x => x.GetUserReviews(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReviewDatUtils_WhenAddUserReviewIsCalled_ShouldCallGetUserReviewsAndReturnIEnumerableIReview()
        {
            var reviewService = new ReviewService(reviewAddHelperMock.Object, reviewDataUtilsMock.Object, reviewFactoryMock.Object);
            var reviews = reviewService.GetUserReviews("username");

            Assert.That(reviews, Is.InstanceOf<IEnumerable<IReview>>());
        }
    }
}
