using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Utils;

namespace OnTheRoad.Logic.Tests.Utils
{
    [TestFixture]
    public class ReviewDataUtilsTests
    {
        [Test]
        public void ReviewDataUtils_WhenInitializedWithNullForReviewRepository_ShouldThrowArgumentNullException()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new ReviewDataUtils(null, unitOfWorkMock.Object));
        }

        [Test]
        public void ReviewDataUtils_WhenInitializedWithNullForReviewRepository_ShouldThrowArgumentNullExceptionWithProperMessage()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewDataUtils(null, unitOfWorkMock.Object));
            StringAssert.Contains("reviewRepository cannot be null!", exc.Message);
        }

        [Test]
        public void ReviewDataUtils_WhenInitializedWithNullForUnitOfWork_ShouldThrowArgumentNullException()
        {
            var reviewRepositoryMock = new Mock<IReviewRepository>();

            Assert.Throws<ArgumentNullException>(() => new ReviewDataUtils(reviewRepositoryMock.Object, null));
        }

        [Test]
        public void ReviewDataUtils_WhenInitializedWithNullForUnitOfWork_ShouldThrowArgumentNullExceptionWithProperMessage()
        {
            var reviewRepositoryMock = new Mock<IReviewRepository>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewDataUtils(reviewRepositoryMock.Object, null));
            StringAssert.Contains("unitOfWork cannot be null!", exc.Message);
        }

        [Test]
        public void ReviewRepository_WhenAddUserReviewIsCalled_ShouldCallAddExactlyOnce()
        {
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var reviewMock = new Mock<IReview>();

            var reviewDataUtils = new ReviewDataUtils(reviewRepositoryMock.Object, unitOfWorkMock.Object);
            reviewDataUtils.AddUserReview(reviewMock.Object);

            reviewRepositoryMock.Verify(x => x.Add(It.IsAny<IReview>()), Times.Once);
        }

        [Test]
        public void ReviewRepository_WhenAddUserReviewIsCalled_ShouldCallAddWithThePassedReview()
        {
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var reviewMock = new Mock<IReview>();

            var reviewDataUtils = new ReviewDataUtils(reviewRepositoryMock.Object, unitOfWorkMock.Object);
            reviewDataUtils.AddUserReview(reviewMock.Object);

            reviewRepositoryMock.Verify(x => x.Add(reviewMock.Object), Times.Once);
        }

        [Test]
        public void UnitOfWork_WhenAddUserReviewIsCalled_ShouldCallCommitExactlyOnce()
        {
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var reviewMock = new Mock<IReview>();

            var reviewDataUtils = new ReviewDataUtils(reviewRepositoryMock.Object, unitOfWorkMock.Object);
            reviewDataUtils.AddUserReview(reviewMock.Object);

            unitOfWorkMock.Verify(x => x.Commit());
        }

        [Test]
        public void ReviewRepository_WhenGetUserReviewsIsCalled_ShouldCallGetByToUserExactlyOnce()
        {
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var reviewDataUtils = new ReviewDataUtils(reviewRepositoryMock.Object, unitOfWorkMock.Object);
            reviewDataUtils.GetUserReviews("someUser");

            reviewRepositoryMock.Verify(x => x.GetByToUser(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReviewRepository_WhenGetUserReviewsIsCalled_ShouldCallGetByToUserWithThePassedUsername()
        {
            var username = "someUser";
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var reviewDataUtils = new ReviewDataUtils(reviewRepositoryMock.Object, unitOfWorkMock.Object);
            reviewDataUtils.GetUserReviews(username);

            reviewRepositoryMock.Verify(x => x.GetByToUser(username), Times.Once);
        }

        [Test]
        public void ReviewRepository_WhenGetUserReviewsIsCalled_ShouldReturnIEnumerableFromIReviews()
        {
            var username = "someUser";
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            reviewRepositoryMock.Setup(x => x.GetByToUser(It.IsAny<string>())).Returns(new List<IReview>());

            var reviewDataUtils = new ReviewDataUtils(reviewRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = reviewDataUtils.GetUserReviews(username);

            Assert.That(actual, Is.InstanceOf<IEnumerable<IReview>>());
        }
    }
}