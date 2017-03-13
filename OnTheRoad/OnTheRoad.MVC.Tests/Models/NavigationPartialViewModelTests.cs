using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.MVC.Models;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Tests.Models
{
    [TestFixture]
    public class NavigationPartialViewModelTests
    {
        [Test]
        public void Constructor_ShouldReturnNavigationPartialViewModelInstance()
        {
            // Arrange & Act & Assert
            Assert.IsInstanceOf<NavigationPartialViewModel>(new NavigationPartialViewModel());
        }

        [Test]
        public void Trips_ShouldGetSetCorrectValue()
        {
            // Arrange
            var navigatioPartialViewModel = new NavigationPartialViewModel();
            var categories = new List<ICategory>();

            // Act
            navigatioPartialViewModel.Categories = categories;

            // Assert
            Assert.AreSame(categories, navigatioPartialViewModel.Categories);
        }
    }
}
