using System;
using Moq;
using NUnit.Framework;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class CategoryOverviewPresenterTests
    {
        [Test]
        public void WhenCategoryOverviewPresenterIsInitialized_WithNull_ArgumentNullException_ShouldBeThrown()
        {
            var categoryOverviewViewMock = new Mock<ICategoryOverviewView>();

            Assert.Throws<ArgumentNullException>(() => new CategoryOverviewPresenter(categoryOverviewViewMock.Object, null));
        }

        [Test]
        public void TripService_WhenGetTripsIsRaised_ShouldCallGetTripsByCategoryNameOrderedByDateExactlyOnce()
        {
            var categoryOverviewViewMock = new Mock<ICategoryOverviewView>();
            var tripServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<TripsModel>();
            categoryOverviewViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoryOverviewPresenter(categoryOverviewViewMock.Object, tripServiceMock.Object);
            categoryOverviewViewMock.Raise(x => x.GetTrips += null, null, new CategoryOverviewEventArgs());

            tripServiceMock.Verify(x => x.GetTripsByCategoryNameOrderedByDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void TripService_WhenGetTripsIsRaised_ShouldCallGetTripsByCategoryNameOrderedByDateWithTheCorrectParams()
        {
            var categoryOverviewViewMock = new Mock<ICategoryOverviewView>();
            var tripServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<TripsModel>();
            categoryOverviewViewMock.Setup(x => x.Model).Returns(modelMock.Object);
            var name = "_";
            var tripsCount = 4;

            var presenter = new CategoryOverviewPresenter(categoryOverviewViewMock.Object, tripServiceMock.Object);
            categoryOverviewViewMock.Raise(x => x.GetTrips += null, null, new CategoryOverviewEventArgs() { CategoryName = name });

            tripServiceMock.Verify(x => x.GetTripsByCategoryNameOrderedByDate(name, tripsCount, It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void ViewModel_WhenGetTripsIsRaised_ShouldBeAssignedTrips()
        {
            var categoryOverviewViewMock = new Mock<ICategoryOverviewView>();
            var tripServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<TripsModel>();
            categoryOverviewViewMock.Setup(x => x.Model).Returns(modelMock.Object);
            var trips = new List<ITrip>() { new Mock<ITrip>().Object };
            tripServiceMock.Setup(x => x.GetTripsByCategoryNameOrderedByDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(trips);

            var presenter = new CategoryOverviewPresenter(categoryOverviewViewMock.Object, tripServiceMock.Object);
            categoryOverviewViewMock.Raise(x => x.GetTrips += null, null, new CategoryOverviewEventArgs());

            Assert.That(categoryOverviewViewMock.Object.Model.Trips.Equals(trips));
        }

        [Test]
        public void ViewModel_WhenGetTripsIsRaised_ShouldBeSetToInstanceOfIEnumerableFromITrip()
        {
            var categoryOverviewViewMock = new Mock<ICategoryOverviewView>();
            var tripServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<TripsModel>();
            categoryOverviewViewMock.Setup(x => x.Model).Returns(modelMock.Object);
            var trips = new List<ITrip>() { new Mock<ITrip>().Object };
            tripServiceMock.Setup(x => x.GetTripsByCategoryNameOrderedByDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(trips);

            var presenter = new CategoryOverviewPresenter(categoryOverviewViewMock.Object, tripServiceMock.Object);
            categoryOverviewViewMock.Raise(x => x.GetTrips += null, null, new CategoryOverviewEventArgs());

            Assert.That(categoryOverviewViewMock.Object.Model.Trips, Is.InstanceOf<IEnumerable<ITrip>>());
        }
    }
}