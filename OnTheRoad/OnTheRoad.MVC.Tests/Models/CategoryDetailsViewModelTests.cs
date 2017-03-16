using NUnit.Framework;
using OnTheRoad.MVC.Models;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Tests.Models
{
    [TestFixture]
    public class CategoryDetailsViewModelTests
    {
        [Test]
        public void Constructor_ShouldReturnCategoryDetailsViewModelInstance()
        {
            // Arrange & Act & Assert
            Assert.IsInstanceOf<CategoryDetailsViewModel>(new CategoryDetailsViewModel());
        }

        [Test]
        public void Name_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new CategoryDetailsViewModel();
            var name = "SomeName";

            // Act
            categoryDetailsModel.Name = name;

            // Assert
            Assert.AreEqual(name, categoryDetailsModel.Name);
        }

        [Test]
        public void Trips_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryDetailsModel = new CategoryDetailsViewModel();
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
            var categoryDetailsModel = new CategoryDetailsViewModel();
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
            var categoryDetailsModel = new CategoryDetailsViewModel();
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
            var categoryDetailsModel = new CategoryDetailsViewModel();
            var total = 5;

            // Act
            categoryDetailsModel.Total = total;

            // Assert
            Assert.AreEqual(total, categoryDetailsModel.Total);
        }
    }
}
