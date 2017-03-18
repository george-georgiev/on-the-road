using NUnit.Framework;
using OnTheRoad.MVC.Models;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Tests.Models
{
    [TestFixture]
    public class TripsWithPagingViewModelTests
    {
        [Test]
        public void Constructor_ShouldReturnTripsWithPagingViewModelInstance()
        {
            // Arrange & Act & Assert
            Assert.IsInstanceOf<TripsWithPagingViewModel>(new TripsWithPagingViewModel());
        }

        [Test]
        public void Heading_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new TripsWithPagingViewModel();
            var heading = "Heading";

            // Act
            categoryDetailsModel.Heading = heading;

            // Assert
            Assert.AreEqual(heading, categoryDetailsModel.Heading);
        }

        [Test]
        public void PageHyperLink_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new TripsWithPagingViewModel();
            var pageHyperLink = "PageHyperLynk";

            // Act
            categoryDetailsModel.PageHyperLink = pageHyperLink;

            // Assert
            Assert.AreEqual(pageHyperLink, categoryDetailsModel.PageHyperLink);
        }

        [Test]
        public void Trips_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new TripsWithPagingViewModel();
            var trips = new List<TripViewModel>();

            // Act
            categoryDetailsModel.Trips = trips;

            // Assert
            Assert.AreSame(trips, categoryDetailsModel.Trips);
        }

        [Test]
        public void Take_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new TripsWithPagingViewModel();
            var take = 5;

            // Act
            categoryDetailsModel.Take = take;

            // Assert
            Assert.AreEqual(take, categoryDetailsModel.Take);
        }

        [Test]
        public void Page_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new TripsWithPagingViewModel();
            var page = 5;

            // Act
            categoryDetailsModel.Page = page;

            // Assert
            Assert.AreEqual(page, categoryDetailsModel.Page);
        }

        [Test]
        public void Total_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new TripsWithPagingViewModel();
            var total = 5;

            // Act
            categoryDetailsModel.Total = total;

            // Assert
            Assert.AreEqual(total, categoryDetailsModel.Total);
        }
    }
}
