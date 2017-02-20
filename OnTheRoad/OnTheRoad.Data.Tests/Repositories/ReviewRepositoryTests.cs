using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;
using System.Collections.ObjectModel;

namespace OnTheRoad.Data.Tests.Repositories
{
    [TestFixture]
    public class ReviewRepositoryTests
    {
        private Mock<IOnTheRoadDbContext> contextMock;
        private Mock<DbSet<Review>> dbSetMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.contextMock = new Mock<IOnTheRoadDbContext>();
            this.dbSetMock = new Mock<DbSet<Review>>();
            contextMock.Setup(x => x.Set<Review>()).Returns(dbSetMock.Object);
        }

        [Test]
        public void ReviewRepository_WhenInitializedWithNullForContext_ShouldThrowNewArgumentExeption()
        {
            Assert.Throws<ArgumentNullException>(() => new ReviewRepository(null));
        }

        [Test]
        public void ReviewRepository_WhenInitializedWithNullForContext_ShouldThrowProperExeptionMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new ReviewRepository(null));
            StringAssert.Contains("context cannot be null!", exc.Message);
        }

        [Test]
        public void GetByToUser_WhenCalledWithNonExistingUser_ShouldReturnNull()
        {
            var nonExistingUser = "Scorpion";
            var username = "Subz-Zero";
            var user = new User() { UserName = username };

            var dbSetUserMock = new Mock<DbSet<User>>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetUserMock.Object);
            var fakeUserData = new List<User>() { user }.AsQueryable();
            SetDbSetUserAsQueryable(fakeUserData, dbSetUserMock);
            this.contextMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);

            var reviewRepository = new ReviewRepository(this.contextMock.Object);
            var actual = reviewRepository.GetByToUser(nonExistingUser);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetByToUser_WhenCalled_ShouldReturnInstanceOfIEnumerableIReview()
        {
            var username = "Subz-Zero";
            var user = new User() { UserName = username };
            var review = new Review() { ToUser = user };
            var fakeReviewsData = new List<Review>() { review }.AsQueryable();
            this.SetDbSetReviewAsQueryable(fakeReviewsData);

            var dbSetUserMock = new Mock<DbSet<User>>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetUserMock.Object);
            var fakeUserData = new List<User>() { user }.AsQueryable();
            SetDbSetUserAsQueryable(fakeUserData, dbSetUserMock);
            this.contextMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);

            var reviewRepository = new ReviewRepository(this.contextMock.Object);
            var actual = reviewRepository.GetByToUser(username);

            Assert.That(actual, Is.InstanceOf<IEnumerable<IReview>>());
        }

        [TestCase("Sub-Zero", "Scorpion", "Mortal Combat", "Positive")]
        [TestCase("One", "Two", "Three", "Neutral")]
        [TestCase("Reptile", "Syrax", "Mortal Combat Ultimat", "Negative")]
        [Test]
        public void GetByToUser_WhenCalled_ShouldReturnInstanceOfIReviewWithCorrectParameters(string toUserUsername, string fromUserUsername, string reviewContent, string ratingValue)
        {
            var postDate = DateTime.Now;
            var toUser = new User() { UserName = toUserUsername };
            var fromUser = new User() { UserName = fromUserUsername };
            var rating = new Rating() { Value = ratingValue };
            var review = new Review() { ToUser = toUser, FromUser = fromUser, ReviewContent = reviewContent, PostingDate = postDate, Rating = rating };
            var fakeData = new List<Review>() { review }.AsQueryable();
            this.SetDbSetReviewAsQueryable(fakeData);
            
            var user = new User() { UserName = toUserUsername };
            var dbSetUserMock = new Mock<DbSet<User>>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetUserMock.Object);
            var fakeUserData = new List<User>() { user }.AsQueryable();
            SetDbSetUserAsQueryable(fakeUserData, dbSetUserMock);
            this.contextMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);

            var reviewRepository = new ReviewRepository(this.contextMock.Object);
            var actual = reviewRepository.GetByToUser(toUserUsername).First();

            Assert.That(actual.FromUser.Username.Equals(fromUserUsername));
            Assert.That(actual.ToUser.Username.Equals(toUserUsername));
            Assert.That(actual.ReviewContent.Equals(reviewContent));
            Assert.That(actual.PostingDate.Equals(postDate));
            Assert.That(actual.Rating.Value.Equals(ratingValue));
        }

        [Test]
        public void Context_WhenIsCalledAdd_ShouldCallSetEntryStateExactlyOnce()
        {
            var reviewMock = new Mock<IReview>();
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<Review>(), It.IsAny<EntityState>())).Verifiable();
            var observableCollection = new ObservableCollection<Review>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            var reviewRepository = new ReviewRepository(this.contextMock.Object);
            reviewRepository.Add(reviewMock.Object);

            this.contextMock.Verify(x => x.SetEntryState(It.IsAny<Review>(), It.IsAny<EntityState>()), Times.Once);
        }

        private void SetDbSetReviewAsQueryable(IQueryable<Review> fakeData)
        {
            this.dbSetMock.As<IQueryable<Review>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            this.dbSetMock.As<IQueryable<Review>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            this.dbSetMock.As<IQueryable<Review>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            this.dbSetMock.As<IQueryable<Review>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());
        }

        private void SetDbSetUserAsQueryable(IQueryable<User> fakeUserData, Mock<DbSet<User>> dbSetUserMock)
        {
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(fakeUserData.Provider);
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(fakeUserData.Expression);
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(fakeUserData.ElementType);
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(fakeUserData.GetEnumerator());
        }
    }
}
