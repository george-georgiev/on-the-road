using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class ReviewPresenterTests
    {
        [Test]
        public void ReviewPresenter_WhenInitializedWithNullForReviewService_ShouldThrowNewArgumentExeption()
        {
            var reviewsViewMock = new Mock<IReviewsView>();

            Assert.Throws<ArgumentNullException>(() => new ReviewsPresenter(reviewsViewMock.Object, null));
        }

        [Test]
        public void ReviewPresenter_WhenInitializedWithNullForReviewService_ShouldThrowProperExeptionMessage()
        {
            var reviewsViewMock = new Mock<IReviewsView>();

            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewsPresenter(reviewsViewMock.Object, null));
            StringAssert.Contains("reviewService cannot be null.", exc.Message);
        }

        [Test]
        public void ReviewService_WhenGetReviewsIsRaise_ShouldCallGetUsersReviewsExactlyOnce()
        {
            var reviewsViewMock = new Mock<IReviewsView>();
            var reviewServiceMock = new Mock<IReviewService>();
            var reviewsModelMock = new Mock<ReviewsModel>();
            reviewsViewMock.Setup(x => x.Model).Returns(reviewsModelMock.Object);

            var presenter = new ReviewsPresenter(reviewsViewMock.Object, reviewServiceMock.Object);
            reviewsViewMock.Raise(x => x.GetReviews += null, null, new GetUserReviewsEventArgs() { Username = "Sub-Zero" });

            reviewServiceMock.Verify(x => x.GetUserReviews(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReviewViewModel_WhenGetReviewsIsRaise_ShouldSetCorrectCollectionOfReviews()
        {
            var reviewsViewMock = new Mock<IReviewsView>();
            var reviewServiceMock = new Mock<IReviewService>();
            var reviewsModelMock = new Mock<ReviewsModel>();
            reviewsViewMock.Setup(x => x.Model).Returns(reviewsModelMock.Object);
            var reviewMock = new Mock<IReview>();
            var reviews = new List<IReview>() { reviewMock.Object };
            reviewServiceMock.Setup(x => x.GetUserReviews(It.IsAny<string>())).Returns(reviews);

            var presenter = new ReviewsPresenter(reviewsViewMock.Object, reviewServiceMock.Object);
            reviewsViewMock.Raise(x => x.GetReviews += null, null, new GetUserReviewsEventArgs() { Username = "Sub-Zero" });

            Assert.That(reviewsViewMock.Object.Model.Reviews.Equals(reviews));
        }

        [Test]
        public void ReviewService_WhenAddReviewIsRaise_ShouldCallAddUserReviewExactlyOnce()
        {
            var reviewsViewMock = new Mock<IReviewsView>();
            var reviewServiceMock = new Mock<IReviewService>();

            var presenter = new ReviewsPresenter(reviewsViewMock.Object, reviewServiceMock.Object);
            reviewsViewMock.Raise(x => x.AddReview += null, null, new AddReviewEventArgs());

            reviewServiceMock.Verify(x => x.AddUserReview(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RatingEnum>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
