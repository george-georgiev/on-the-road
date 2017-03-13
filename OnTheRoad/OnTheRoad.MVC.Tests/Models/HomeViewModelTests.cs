using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.MVC.Models;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Tests.Models
{
    [TestFixture]
    public class HomeViewModelTests
    {
        [Test]
        public void Constructor_ShouldReturnHomeViewModelInstance()
        {
            // Arrange & Act & Assert
            Assert.IsInstanceOf<HomeViewModel>(new HomeViewModel());
        }

        [Test]
        public void Trips_ShouldGetSetCorrectValue()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();
            var trips = new List<TripViewModel>();

            // Act
            homeViewModel.Trips = trips;

            // Assert
            Assert.AreSame(trips, homeViewModel.Trips);
        }

        [Test]
        public void AllTripsCount_ShouldGetSetCorrectValue()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();
            var tripsCount = 5;

            // Act
            homeViewModel.AllTripsCount = tripsCount;

            // Assert
            Assert.AreEqual(tripsCount, homeViewModel.AllTripsCount);
        }

        [Test]
        public void AllUsersCount_ShouldGetSetCorrectValue()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();
            var usersCount = 5;

            // Act
            homeViewModel.AllUsersCount = usersCount;

            // Assert
            Assert.AreEqual(usersCount, homeViewModel.AllUsersCount);
        }
    }
}
