using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using System;
using System.Collections.Generic;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class TripsPresenterTests
    {
        [Test]
        public void Constructor_WhenTripGetServiceIsNull_ShouldThrow()
        {
            var viewMock = new Mock<ITripsView>();
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            Assert.Throws<ArgumentNullException>(() => new TripsPresenter(viewMock.Object, null, subscriptionServiceMock.Object));
        }

        [Test]
        public void Constructor_WhenSubscriptionServiceIsNull_ShouldThrow()
        {
            var viewMock = new Mock<ITripsView>();
            var tripGetServiceMock = new Mock<ITripGetService>();

            Assert.Throws<ArgumentNullException>(() => new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, null));
        }

        [Test]
        public void GetTripsTotalCount_WhenIsRaised_ShouldTCallGetTripsCount()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            tripGetServiceMock.Setup(x => x.GetTripsCount());
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new GetTripsEventArgs();

            viewMock.Raise(x => x.GetTripsTotalCount += null, null, e);

            tripGetServiceMock.Verify(x => x.GetTripsCount(), Times.Once);
        }

        [Test]
        public void GetTripsTotalCount_WhenIsRaised_ShouldSetModelTripsTotalCount()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var count = 5;
            tripGetServiceMock.Setup(x => x.GetTripsCount()).Returns(count);
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new GetTripsEventArgs();

            viewMock.Raise(x => x.GetTripsTotalCount += null, null, e);

            Assert.AreEqual(count, model.TripsTotalCount);
        }

        [Test]
        public void GetTrips_WhenIsRaised_ShouldCallGetTrips()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var skip = 1;
            var take = 2;
            tripGetServiceMock.Setup(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>()));
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new GetTripsEventArgs() { Skip = skip, Take = take };

            viewMock.Raise(x => x.GetTrips += null, null, e);

            tripGetServiceMock.Verify(x => x.GetTrips(It.Is<int>(n => n == skip), It.Is<int>(n => n == take)), Times.Once);
        }

        [Test]
        public void GetTrips_WhenIsRaised_ShouldSetModelTrips()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var skip = 1;
            var take = 2;
            var trips = new List<ITrip>() { new Mock<ITrip>().Object };
            tripGetServiceMock.Setup(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new GetTripsEventArgs() { Skip = skip, Take = take };

            viewMock.Raise(x => x.GetTrips += null, null, e);

            Assert.AreEqual(model.Trips, trips);
        }

        [Test]
        public void GetTripsBySearchPattern_WhenIsRaised_ShouldCallGetTripsBySearchPattern()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var pattern = "pattern";
            var skip = 1;
            var take = 2;
            tripGetServiceMock.Setup(x => x.GetTripsBySearchPattern(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new SearchTripsEventArgs() { Skip = skip, Take = take, SearchPattern = pattern };

            viewMock.Raise(x => x.GetTripsBySearchPattern += null, null, e);

            tripGetServiceMock.Verify(x => x.GetTripsBySearchPattern(It.Is<string>(n => n == pattern), It.Is<int>(n => n == skip), It.Is<int>(n => n == take)), Times.Once);
        }

        [Test]
        public void GetTripsBySearchPattern_WhenIsRaised_ShouldSetModelTrips()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var trips = new List<ITrip>() { new Mock<ITrip>().Object };
            var pattern = "pattern";
            var skip = 1;
            var take = 2;
            tripGetServiceMock.Setup(x => x.GetTripsBySearchPattern(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new SearchTripsEventArgs() { Skip = skip, Take = take, SearchPattern = pattern };

            viewMock.Raise(x => x.GetTripsBySearchPattern += null, null, e);

            Assert.AreEqual(model.Trips, trips);
        }

        [Test]
        public void GetTripsSearchTotalCount_WhenIsRaised_ShouldCallGetTripsCountBySearchPattern()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var pattern = "pattern";
            tripGetServiceMock.Setup(x => x.GetTripsCountBySearchPattern(It.IsAny<string>()));
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new SearchTripsEventArgs() { SearchPattern = pattern };

            viewMock.Raise(x => x.GetTripsSearchTotalCount += null, null, e);

            tripGetServiceMock.Verify(x => x.GetTripsCountBySearchPattern(It.Is<string>(n => n == pattern)), Times.Once);
        }

        [Test]
        public void GetTripsSearchTotalCount_WhenIsRaised_ShouldCallSetModelTripsTotalCount()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var pattern = "pattern";
            var count = 5;
            tripGetServiceMock.Setup(x => x.GetTripsCountBySearchPattern(It.IsAny<string>())).Returns(count);
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new SearchTripsEventArgs() { SearchPattern = pattern };

            viewMock.Raise(x => x.GetTripsSearchTotalCount += null, null, e);

            Assert.AreEqual(count, model.TripsTotalCount);
        }

        [Test]
        public void Subscribe_WhenIsRaised_ShouldCallAddOrUpdateSubscription()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var currentUserName = "CurrentUsername";
            var tripId = 5;
            var subscriptionStatus = SubscriptionStatus.Attending;
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(x => x.AddOrUpdateSubscription(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<SubscriptionStatus>()));
            var tripGetServiceMock = new Mock<ITripGetService>();
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new SubscribeEventArgs() { CurrentUserName = currentUserName, TripId = tripId, SubscriptionStatus = subscriptionStatus };

            viewMock.Raise(x => x.Subscribe += null, null, e);

            subscriptionServiceMock.Verify(x => x.AddOrUpdateSubscription(It.Is<string>(n => n == currentUserName), It.Is<int>(n => n == tripId), It.Is<SubscriptionStatus>(n => n == subscriptionStatus)), Times.Once);
        }

        [Test]
        public void GetTrip_WhenIsRaised_ShouldCallGetTripById()
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var tripId = 5;
            var tripMock = new Mock<ITrip>();
            tripMock.Setup(x => x.Subscriptions).Returns(new List<ISubscription>());
            var organiserMock = new Mock<IUser>();
            organiserMock.Setup(x => x.Username).Returns("username");
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripGetServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new GetTripEventArgs() { TripId = tripId };

            viewMock.Raise(x => x.GetTrip += null, null, e);

            tripGetServiceMock.Verify(x => x.GetTripById(It.Is<int>(n => n == tripId)), Times.Once);
        }

        [TestCase("username", "username2")]
        [TestCase("username", "username")]
        public void GetTrip_WhenIsRaised_ShouldSetModelIsOrganiser(string currentUsername, string organiserUsername)
        {
            var viewMock = new Mock<ITripsView>();
            var model = new TripsModel();
            viewMock.Setup(x => x.Model).Returns(model);
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var tripId = 5;
            var tripMock = new Mock<ITrip>();
            tripMock.Setup(x => x.Subscriptions).Returns(new List<ISubscription>());
            var organiserMock = new Mock<IUser>();
            organiserMock.Setup(x => x.Username).Returns(organiserUsername);
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripGetServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);
            var presenter = new TripsPresenter(viewMock.Object, tripGetServiceMock.Object, subscriptionServiceMock.Object);
            var e = new GetTripEventArgs() { TripId = tripId, CurrentUserName = currentUsername };

            viewMock.Raise(x => x.GetTrip += null, null, e);

            Assert.AreEqual(currentUsername == organiserUsername, model.IsOrganiser);
        }
    }
}
