using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class TripServiceTests
    {
        [Test]
        public void Constructor_WhenTripDataUtilIsNull_ShouldThrow()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();

            Assert.Throws<ArgumentNullException>(() => new TripService(null, tripAddHelperMock.Object));
        }

        [Test]
        public void Constructor_WhenTripAddHelperIsNull_ShouldThrow()
        {
            var tripDataUtilMock = new Mock<ITripDataUtil>();

            Assert.Throws<ArgumentNullException>(() => new TripService(tripDataUtilMock.Object, null));
        }

        [Test]
        public void AddTrip_ShouldSetTripCategoriesById()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripMock = new Mock<ITrip>();
            var categoryIds = new List<int>() { 1 };
            var username = "username";
            var tagNames = new List<string>() { "tagName" };
            tripAddHelperMock.Setup(x => x.SetTripCategoriesById(It.IsAny<ITrip>(), It.IsAny<IEnumerable<int>>()));
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.AddTrip(tripMock.Object, username, categoryIds, tagNames);

            tripAddHelperMock.Verify(x => x.SetTripCategoriesById(It.Is<ITrip>(t => t.Equals(tripMock.Object)), It.Is<IEnumerable<int>>(t => t.Equals(categoryIds))), Times.Once);
        }

        [Test]
        public void AddTrip_ShouldSetTripTagsByName()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripMock = new Mock<ITrip>();
            var categoryIds = new List<int>() { 1 };
            var username = "username";
            var tagNames = new List<string>() { "tagName" };
            tripAddHelperMock.Setup(x => x.SetTripTagsByName(It.IsAny<ITrip>(), It.IsAny<IEnumerable<string>>()));
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.AddTrip(tripMock.Object, username, categoryIds, tagNames);

            tripAddHelperMock.Verify(x => x.SetTripTagsByName(It.Is<ITrip>(t => t.Equals(tripMock.Object)), It.Is<IEnumerable<string>>(t => t.Equals(tagNames))), Times.Once);
        }

        [Test]
        public void AddTrip_ShouldSetTripOrganiserByUsername()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripMock = new Mock<ITrip>();
            var categoryIds = new List<int>() { 1 };
            var username = "username";
            var tagNames = new List<string>() { "tagName" };
            tripAddHelperMock.Setup(x => x.SetTripOrganiserByUsername(It.IsAny<ITrip>(), It.IsAny<string>()));
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.AddTrip(tripMock.Object, username, categoryIds, tagNames);

            tripAddHelperMock.Verify(x => x.SetTripOrganiserByUsername(It.Is<ITrip>(t => t.Equals(tripMock.Object)), It.Is<string>(t => t.Equals(username))), Times.Once);
        }

        [Test]
        public void AddTrip_ShouldAddTrip()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripMock = new Mock<ITrip>();
            var categoryIds = new List<int>() { 1 };
            var username = "username";
            var tagNames = new List<string>() { "tagName" };
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            tripDataUtilMock.Setup(x => x.AddTrip(It.IsAny<ITrip>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);
            
            tripService.AddTrip(tripMock.Object, username, categoryIds, tagNames);

            tripDataUtilMock.Verify(x => x.AddTrip(It.Is<ITrip>(t => t.Equals(tripMock.Object))), Times.Once);
        }

        [Test]
        public void GetTripsByCategoryName_ShouldCallGetTripsByCategoryName()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var categoryName = "CategoryName";
            var skip = 1;
            var take = 2;
            tripDataUtilMock.Setup(x => x.GetTripsByCategoryName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripsByCategoryName(categoryName, skip, take);

            tripDataUtilMock.Verify(x => x.GetTripsByCategoryName(It.Is<string>(t => t.Equals(categoryName)), It.Is<int>(t => t == skip), It.Is<int>(t => t == take)), Times.Once);
        }

        [Test]
        public void GetTripsByCategoryName_ShouldReturnCorrectTrips()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var categoryName = "CategoryName";
            var skip = 1;
            var take = 2;
            var trip = new Mock<ITrip>();
            var trips = new List<ITrip>() { trip.Object };
            tripDataUtilMock.Setup(x => x.GetTripsByCategoryName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripsByCategoryName(categoryName, skip, take).ToList();

            Assert.AreEqual(trips[0], result[0]);
        }

        [Test]
        public void GetTripsByCategoryNameOrderedByDate_ShouldCallGetTripsByCategoryNameOrderedByDate()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var categoryName = "CategoryName";
            var count = 1;
            var isAscending = false;
            tripDataUtilMock.Setup(x => x.GetTripsByCategoryNameOrderedByDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending);

            tripDataUtilMock.Verify(x => x.GetTripsByCategoryNameOrderedByDate(It.Is<string>(t => t.Equals(categoryName)), It.Is<int>(t => t == count), It.Is<bool>(t => t == isAscending)), Times.Once);
        }

        [Test]
        public void GetTripsByCategoryNameOrderedByDate_ShouldReturnCorrectTrips()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var categoryName = "CategoryName";
            var count = 1;
            var isAscending = false;
            var trip = new Mock<ITrip>();
            var trips = new List<ITrip>() { trip.Object };
            tripDataUtilMock.Setup(x => x.GetTripsByCategoryNameOrderedByDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(trips);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending).ToList();

            Assert.AreEqual(trips[0], result[0]);
        }

        [Test]
        public void GetTripsCountByCategoryName_ShouldCallGetTripsCountByCategoryName()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var categoryName = "CategoryName";
            tripDataUtilMock.Setup(x => x.GetTripsCountByCategoryName(It.IsAny<string>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripsCountByCategoryName(categoryName);

            tripDataUtilMock.Verify(x => x.GetTripsCountByCategoryName(It.Is<string>(t => t.Equals(categoryName))), Times.Once);
        }

        [Test]
        public void GetTripsCountByCategoryName_ShouldReturnCorrectTripsCount()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var categoryName = "CategoryName";
            var count = 5;
            tripDataUtilMock.Setup(x => x.GetTripsCountByCategoryName(It.IsAny<string>())).Returns(count);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripsCountByCategoryName(categoryName);

            Assert.AreEqual(count, result);
        }

        [Test]
        public void GetTripById_ShouldCallGetTripById()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var id = 5;
            tripDataUtilMock.Setup(x => x.GetTripById(It.IsAny<int>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripById(id);

            tripDataUtilMock.Verify(x => x.GetTripById(It.Is<int>(t => t == id)), Times.Once);
        }

        [Test]
        public void GetTripById_ShouldReturnCorrectTrip()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var id = 5;
            var tripMock = new Mock<ITrip>();
            tripDataUtilMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripById(id);

            Assert.AreEqual(tripMock.Object, result);
        }

        [Test]
        public void GetTripsBySearchPattern_ShouldCallGetTripsBySearchPattern()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var pattern = "pattern";
            var skip = 1;
            var take = 2;
            tripDataUtilMock.Setup(x => x.GetTripsBySearchPattern(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripsBySearchPattern(pattern, skip, take);

            tripDataUtilMock.Verify(x => x.GetTripsBySearchPattern(It.Is<string>(t => t == pattern), It.Is<int>(t => t == skip), It.Is<int>(t => t == take)), Times.Once);
        }

        [Test]
        public void GetTripsBySearchPattern_ShouldReturnCorrectTrip()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var pattern = "pattern";
            var skip = 1;
            var take = 2;
            var tripMock = new Mock<ITrip>();
            var trips = new List<ITrip>() { tripMock.Object };
            tripDataUtilMock.Setup(x => x.GetTripsBySearchPattern(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripsBySearchPattern(pattern, skip, take).ToList();

            Assert.AreEqual(trips[0], result[0]);
            Assert.AreEqual(trips.Count, result.Count);
        }

        [Test]
        public void GetTripsCountBySearchPattern_ShouldCallGetTripsCountBySearchPattern()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var pattern = "pattern";
            tripDataUtilMock.Setup(x => x.GetTripsCountBySearchPattern(It.IsAny<string>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripsCountBySearchPattern(pattern);

            tripDataUtilMock.Verify(x => x.GetTripsCountBySearchPattern(It.Is<string>(t => t == pattern)), Times.Once);
        }

        [Test]
        public void GetTripsCountBySearchPattern_ShouldReturnCorrectCount()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var pattern = "pattern";
            var count = 5;
            tripDataUtilMock.Setup(x => x.GetTripsCountBySearchPattern(It.IsAny<string>())).Returns(count);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripsCountBySearchPattern(pattern);

            Assert.AreEqual(count, result);
        }

        [Test]
        public void GetTrips_ShouldCallGetTrips()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var skip = 1;
            var take = 2;
            tripDataUtilMock.Setup(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>()));
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTrips(skip, take);

            tripDataUtilMock.Verify(x => x.GetTrips(It.Is<int>(t => t == skip), It.Is<int>(t => t == take)), Times.Once);
        }

        [Test]
        public void GetTrips_ShouldReturnCorrectTrips()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var skip = 1;
            var take = 2;
            var trip = new Mock<ITrip>().Object;
            var trips = new List<ITrip>() { trip };
            tripDataUtilMock.Setup(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTrips(skip, take).ToList();

            Assert.AreEqual(trips[0], result[0]);
            Assert.AreEqual(trips.Count, result.Count);
        }

        [Test]
        public void GetTripsCount_ShouldCallGetTripsCount()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            tripDataUtilMock.Setup(x => x.GetTripsCount());
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            tripService.GetTripsCount();

            tripDataUtilMock.Verify(x => x.GetTripsCount(), Times.Once);
        }

        [Test]
        public void GetTripsCount_ShouldReturnCorrectCount()
        {
            var tripAddHelperMock = new Mock<ITripAddHelper>();
            var tripDataUtilMock = new Mock<ITripDataUtil>();
            var count = 5;
            tripDataUtilMock.Setup(x => x.GetTripsCount()).Returns(count);
            var tripService = new TripService(tripDataUtilMock.Object, tripAddHelperMock.Object);

            var result = tripService.GetTripsCount();

            Assert.AreEqual(count, result);
        }
    }
}
