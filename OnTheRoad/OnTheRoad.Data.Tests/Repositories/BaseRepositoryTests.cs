using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Contracts;
using System.Data.Entity;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Data.Tests.Repositories
{
    /// <summary>
    /// Using RatingRepository to test the BaseRepository functionality since the last is an abstract class.
    /// </summary>
    public class BaseRepositoryTests
    {
        private Mock<IOnTheRoadDbContext> contextMock;
        private Mock<DbSet<Rating>> dbSetMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.contextMock = new Mock<IOnTheRoadDbContext>();
            this.dbSetMock = new Mock<DbSet<Rating>>();
            contextMock.Setup(x => x.Set<Rating>()).Returns(dbSetMock.Object);
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

        [Test]
        public void WhenIsCalledAddWithNullForModel_ShouldThrowNewArgumentException()
        {
            var ratingRepository = new RatingRepository(this.contextMock.Object);

            Assert.Throws<ArgumentNullException>(() => ratingRepository.Add(null));
        }

        [Test]
        public void WhenIsCalledAddWithNullForModel_ShouldThrowNewArgumentExceptionWithProperMessage()
        {
            var ratingRepository = new RatingRepository(this.contextMock.Object);

            var exc = Assert.Throws<ArgumentNullException>(() => ratingRepository.Add(null));
            StringAssert.Contains("model can not be null!", exc.Message);
        }

        [Test]
        public void Context_WhenIsCalledAdd_ShouldCallSetEntryStateExactlyOnce()
        {
            var ratingMock = new Mock<IRating>();
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<Rating>(), It.IsAny<EntityState>())).Verifiable();
            var observableCollection = new ObservableCollection<Rating>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            var ratingRepository = new RatingRepository(this.contextMock.Object);
            ratingRepository.Add(ratingMock.Object);

            this.contextMock.Verify(x => x.SetEntryState(It.IsAny<Rating>(), It.IsAny<EntityState>()), Times.Once);
        }

        [Test]
        public void WhenIsCalledDeleteWithNullForModel_ShouldThrowNewArgumentException()
        {
            var ratingRepository = new RatingRepository(this.contextMock.Object);

            Assert.Throws<ArgumentNullException>(() => ratingRepository.Delete(null));
        }

        [Test]
        public void WhenIsCalledDeleteWithNullForModel_ShouldThrowNewArgumentExceptionWithProperMessage()
        {
            var ratingRepository = new RatingRepository(this.contextMock.Object);

            var exc = Assert.Throws<ArgumentNullException>(() => ratingRepository.Delete(null));
            StringAssert.Contains("model can not be null!", exc.Message);
        }

        [Test]
        public void Context_WhenIsCalledDelete_ShouldCallSetEntryStateExactlyOnce()
        {
            var ratingMock = new Mock<IRating>();
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<Rating>(), It.IsAny<EntityState>())).Verifiable();
            var observableCollection = new ObservableCollection<Rating>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            var ratingRepository = new RatingRepository(this.contextMock.Object);
            ratingRepository.Delete(ratingMock.Object);

            this.contextMock.Verify(x => x.SetEntryState(It.IsAny<Rating>(), It.IsAny<EntityState>()), Times.Once);
        }

        [Test]
        public void WhenIsCalledUpdateWithNullForModel_ShouldThrowNewArgumentException()
        {
            var ratingRepository = new RatingRepository(this.contextMock.Object);

            Assert.Throws<ArgumentNullException>(() => ratingRepository.Update(null));
        }

        [Test]
        public void WhenIsCalledUpdateWithNullForModel_ShouldThrowNewArgumentExceptionWithProperMessage()
        {
            var ratingRepository = new RatingRepository(this.contextMock.Object);

            var exc = Assert.Throws<ArgumentNullException>(() => ratingRepository.Update(null));
            StringAssert.Contains("model can not be null!", exc.Message);
        }

        [Test]
        public void Context_WhenIsCalledUpdate_ShouldCallSetEntryStateExactlyOnce()
        {
            var ratingMock = new Mock<IRating>();
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<Rating>(), It.IsAny<EntityState>())).Verifiable();
            var observableCollection = new ObservableCollection<Rating>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            var ratingRepository = new RatingRepository(this.contextMock.Object);
            ratingRepository.Update(ratingMock.Object);

            this.contextMock.Verify(x => x.SetEntryState(It.IsAny<Rating>(), It.IsAny<EntityState>()), Times.Once);
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
