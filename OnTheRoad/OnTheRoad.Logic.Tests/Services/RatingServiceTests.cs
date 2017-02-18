using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Logic.Services;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class RatingServiceTests
    {
        [Test]
        public void RatingService_WhenInitializedWithNullForIRatingRepository_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RatingService(null));
        }

        [Test]
        public void RatingService_WhenInitializedWithNullForIRatingRepository_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new RatingService(null));
            StringAssert.Contains("ratingRepository cannot be null!", exc.Message);
        }

        [Test]
        public void WhenGetRatingByValueIsCalled_ShouldReturnInstanceOfIRating()
        {
            var ratingRepositoryMock = new Mock<IRatingRepository>();
            var ratingMock = new Mock<IRating>();
            ratingRepositoryMock.Setup(x => x.GetByValue(It.IsAny<string>())).Returns(ratingMock.Object);

            var ratingService = new RatingService(ratingRepositoryMock.Object);
            var rating = ratingService.GetRatingByValue(RatingEnum.Positive);

            Assert.That(rating, Is.InstanceOf<IRating>());
        }

        [Test]
        public void RatingRepository_WhenGetRatingByValueIsCalled_ShouldCallGetByValueExactlyOnce()
        {
            var ratingRepositoryMock = new Mock<IRatingRepository>();
            var ratingMock = new Mock<IRating>();
            ratingRepositoryMock.Setup(x => x.GetByValue(It.IsAny<string>())).Returns(ratingMock.Object);

            var ratingService = new RatingService(ratingRepositoryMock.Object);
            var rating = ratingService.GetRatingByValue(RatingEnum.Positive);

            ratingRepositoryMock.Verify(x => x.GetByValue(It.IsAny<string>()), Times.Once);
        }
    }
}
