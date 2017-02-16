using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;
using OnTheRoad.Data.Models;
using System.Data.Entity;

namespace OnTheRoad.Data.Tests.Repositories
{
    [TestFixture]
    public class RatingRepositoryTests
    {
        private Mock<OnTheRoadIdentityDbContext> contextMock;
        private Mock<DbSet<Rating>> dbSetMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.contextMock = new Mock<OnTheRoadIdentityDbContext>();
            this.dbSetMock = new Mock<DbSet<Rating>>();
            contextMock.Setup(x => x.Set<Rating>()).Returns(dbSetMock.Object);
        }

        [Test]
        public void RatingRepository_WhenInitializedWithNullForContext_ShouldThrowNewArgumentExeption()
        {
            Assert.Throws<ArgumentNullException>(() => new RatingRepository(null));
        }

        [Test]
        public void RatingRepository_WhenInitializedWithNullForContext_ShouldThrowProperExeptionMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new RatingRepository(null));
            StringAssert.Contains("context cannot be null!", exc.Message);
        }

        [TestCase("Best")]
        [TestCase("Good")]
        [Test]
        public void GetByValue_WhenCalledWithUnExistingRatingType_ShouldThrowArgumentException(string value)
        {
            var positiveRating = new Rating() { Value = "Positive" };
            var neutralRating = new Rating() { Value = "Neutral" };
            var negativeRating = new Rating() { Value = "Negative" };
            var fakeData = new List<Rating>() { positiveRating, neutralRating, negativeRating }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var ratingRepository = new RatingRepository(this.contextMock.Object);

            Assert.Throws<ArgumentException>(() => ratingRepository.GetByValue(value));
        }

        [TestCase("Best")]
        [TestCase("Good")]
        [Test]
        public void GetByValue_WhenCalledWithUnExistingRatingType_ShouldThrowArgumentExceptionWithProperMessage(string value)
        {
            var positiveRating = new Rating() { Value = "Positive" };
            var neutralRating = new Rating() { Value = "Neutral" };
            var negativeRating = new Rating() { Value = "Negative" };
            var fakeData = new List<Rating>() { positiveRating, neutralRating, negativeRating }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var ratingRepository = new RatingRepository(this.contextMock.Object);

            var exc = Assert.Throws<ArgumentException>(() => ratingRepository.GetByValue(value));
            StringAssert.Contains("The provided value doesn't exist in the rating system!", exc.Message);
        }

        [TestCase("Positive")]
        [TestCase("Neutral")]
        [TestCase("Negative")]
        [Test]
        public void GetByValue_WhenCalledWithExistingRatingType_ShouldReturnInstanceOfIRating(string value)
        {
            var positiveRating = new Rating() { Value = "Positive" };
            var neutralRating = new Rating() { Value = "Neutral" };
            var negativeRating = new Rating() { Value = "Negative" };
            var fakeData = new List<Rating>() { positiveRating, neutralRating, negativeRating }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var ratingRepository = new RatingRepository(this.contextMock.Object);
            var actual = ratingRepository.GetByValue(value);

            Assert.That(actual, Is.InstanceOf<IRating>());
        }

        [Test]
        public void GetAll_WhenCalled_ShouldReturnInstanceOfIEnumerableIRating()
        {
            var positiveRating = new Rating() { Value = "Positive" };
            var neutralRating = new Rating() { Value = "Neutral" };
            var negativeRating = new Rating() { Value = "Negative" };
            var fakeData = new List<Rating>() { positiveRating, neutralRating, negativeRating }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var ratingRepository = new RatingRepository(this.contextMock.Object);
            var actual = ratingRepository.GetAll();

            Assert.That(actual, Is.InstanceOf<IEnumerable<IRating>>());
        }

        [Test]
        public void GetById_WhenIsCalled_ShouldReturnInstanceOfIUser()
        {
            var ratingMock = new Mock<Rating>();
            this.dbSetMock.Setup(x => x.Find(It.IsAny<string>())).Returns(ratingMock.Object);

            var ratingRepository = new RatingRepository(this.contextMock.Object);
            var actual = ratingRepository.GetById("id");

            Assert.That(actual, Is.InstanceOf<IRating>());
        }

        private void SetDbSetUserAsQueryable(IQueryable<Rating> fakeData)
        {
            this.dbSetMock.As<IQueryable<Rating>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            this.dbSetMock.As<IQueryable<Rating>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            this.dbSetMock.As<IQueryable<Rating>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            this.dbSetMock.As<IQueryable<Rating>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());
        }
    }
}
