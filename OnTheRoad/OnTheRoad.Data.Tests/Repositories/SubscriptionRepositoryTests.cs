using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using System.Data.Entity;
using System.Linq;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;
using System.Collections.ObjectModel;

namespace OnTheRoad.Data.Tests.Repositories
{
    [TestFixture]
    public class SubscriptionRepositoryTests
    {
        private Mock<IOnTheRoadDbContext> contextMock;
        private Mock<DbSet<Subscription>> dbSetMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.contextMock = new Mock<IOnTheRoadDbContext>();
            this.dbSetMock = new Mock<DbSet<Subscription>>();
            contextMock.Setup(x => x.Set<Subscription>()).Returns(dbSetMock.Object);
        }

        [Test]
        public void WhenGetSubscriptionIsCalledAndSubcriptionIsNotFoundInTheContext_ShouldReturnNull()
        {
            var subscription = new Subscription();
            var user = new User() { UserName = "Sub_Zero" };
            subscription.User = user;
            subscription.TripId = 1;
            var fakeSubscriptionsData = new List<Subscription>() { subscription }.AsQueryable();
            this.SetDbSetReviewAsQueryable(fakeSubscriptionsData);
            this.contextMock.Setup(x => x.Subscriptions).Returns(dbSetMock.Object);

            var repository = new SubscriptionRepository(contextMock.Object);
            var actual = repository.GetSubscription("username", 0);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void WhenGetSubscriptionIsCalledAndThereIsSubcriptionInTheContext_ShouldReturnInstanceOfISubscription()
        {
            var subscription = new Subscription();
            var user = new User() { UserName = "Sub_Zero" };
            subscription.User = user;
            subscription.TripId = 1;
            var fakeSubscriptionsData = new List<Subscription>() { subscription }.AsQueryable();
            this.SetDbSetReviewAsQueryable(fakeSubscriptionsData);
            this.contextMock.Setup(x => x.Subscriptions).Returns(dbSetMock.Object);

            var repository = new SubscriptionRepository(contextMock.Object);
            var actual = repository.GetSubscription("Sub_Zero", 1);

            Assert.That(actual, Is.InstanceOf<ISubscription>());
        }

        [Test]
        public void SetEntityState_WhenAddMethodIsCalledAndWithDomainModelIsNull_ShouldThrowArgumentNullException()
        {
            var repository = new SubscriptionRepository(contextMock.Object);

            Assert.Throws<ArgumentNullException>(() => repository.Add(null));
        }

        [Test]
        public void SetEntityState_WhenAddMethodIsCalledAndWithDomainModelIsNull_ShouldThrowArgumentNullExceptionWithProperException()
        {
            var repository = new SubscriptionRepository(contextMock.Object);

           var exc =  Assert.Throws<ArgumentNullException>(() => repository.Add(null));
            StringAssert.Contains("model can not be null!", exc.Message);
        }

        [Test]
        public void SetEntityState_WhenAddMethodIsCalled_ShouldSetEntityUserToTheOneFromTheContext()
        {
            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Username).Returns("Sub-Zero");
            var tripMock = new Mock<ITrip>();
            tripMock.Setup(x => x.Id).Returns(1);
            var subcriptionModel = new Mock<ISubscription>();
            subcriptionModel.Setup(x => x.User).Returns(userMock.Object);
            subcriptionModel.Setup(x => x.Trip).Returns(tripMock.Object);

            // sets this.DbSet.Local
            var entity = new Subscription();
            var observableCollection = new ObservableCollection<Subscription>(new List<Subscription>() { entity });
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            // sets this.Context.Users
            var user = new User() { UserName = "Sub-Zero" };
            var dbSetUserMock = new Mock<DbSet<User>>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetUserMock.Object);
            var fakeUserData = new List<User>() { user }.AsQueryable();
            SetDbSetUserAsQueryable(fakeUserData, dbSetUserMock);
            this.contextMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);

            // sets this.Context.Trips
            var trip = new Trip() { Id = 1 };
            var dbSetTripMock = new Mock<DbSet<Trip>>();
            contextMock.Setup(x => x.Set<Trip>()).Returns(dbSetTripMock.Object);
            var fakeTripData = new List<Trip>() { trip }.AsQueryable();
            SetDbSetTripAsQueryable(fakeTripData, dbSetTripMock);
            this.contextMock.Setup(x => x.Trips).Returns(dbSetTripMock.Object);

            var repository = new SubscriptionRepository(contextMock.Object);
            repository.Add(subcriptionModel.Object);

            Assert.That(entity.User.Equals(user));
        }

        [Test]
        public void SetEntityState_WhenAddMethodIsCalled_ShouldSetEntityTripToTheOneFromTheContext()
        {
            var userMock = new Mock<IUser>();
            userMock.Setup(x => x.Username).Returns("Sub-Zero");
            var tripMock = new Mock<ITrip>();
            tripMock.Setup(x => x.Id).Returns(1);
            var subcriptionModel = new Mock<ISubscription>();
            subcriptionModel.Setup(x => x.User).Returns(userMock.Object);
            subcriptionModel.Setup(x => x.Trip).Returns(tripMock.Object);

            // sets this.DbSet.Local
            var entity = new Subscription();
            var observableCollection = new ObservableCollection<Subscription>(new List<Subscription>() { entity });
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            // sets this.Context.Users
            var user = new User() { UserName = "Sub-Zero" };
            var dbSetUserMock = new Mock<DbSet<User>>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetUserMock.Object);
            var fakeUserData = new List<User>() { user }.AsQueryable();
            SetDbSetUserAsQueryable(fakeUserData, dbSetUserMock);
            this.contextMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);

            // sets this.Context.Trips
            var trip = new Trip() { Id = 1 };
            var dbSetTripMock = new Mock<DbSet<Trip>>();
            contextMock.Setup(x => x.Set<Trip>()).Returns(dbSetTripMock.Object);
            var fakeTripData = new List<Trip>() { trip }.AsQueryable();
            SetDbSetTripAsQueryable(fakeTripData, dbSetTripMock);
            this.contextMock.Setup(x => x.Trips).Returns(dbSetTripMock.Object);

            var repository = new SubscriptionRepository(contextMock.Object);
            repository.Add(subcriptionModel.Object);

            Assert.That(entity.Trip.Equals(trip));
        }

        private void SetDbSetReviewAsQueryable(IQueryable<Subscription> fakeData)
        {
            this.dbSetMock.As<IQueryable<Subscription>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            this.dbSetMock.As<IQueryable<Subscription>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            this.dbSetMock.As<IQueryable<Subscription>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            this.dbSetMock.As<IQueryable<Subscription>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());
        }

        private void SetDbSetUserAsQueryable(IQueryable<User> fakeUserData, Mock<DbSet<User>> dbSetUserMock)
        {
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(fakeUserData.Provider);
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(fakeUserData.Expression);
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(fakeUserData.ElementType);
            dbSetUserMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(fakeUserData.GetEnumerator());
        }

        private void SetDbSetTripAsQueryable(IQueryable<Trip> fakeTripData, Mock<DbSet<Trip>> dbSetTripMock)
        {
            dbSetTripMock.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(fakeTripData.Provider);
            dbSetTripMock.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(fakeTripData.Expression);
            dbSetTripMock.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(fakeTripData.ElementType);
            dbSetTripMock.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(fakeTripData.GetEnumerator());
        }
    }
}