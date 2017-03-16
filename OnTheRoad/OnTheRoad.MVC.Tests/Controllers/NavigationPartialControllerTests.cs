using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Controllers;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Tests.Controllers
{
    [TestFixture]
    public class NavigationPartialControllerTests
    {
        [Test]
        public void Constructor_WhenCategoryServiceIsNull_ShouldThrow()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NavigationPartialController(null));
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnPartialViewResultInstance()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var navigationPartialController = new NavigationPartialController(categoryServiceMock.Object);

            // Act
            var result = navigationPartialController.Index();

            // Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnCorrectPartialView()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var navigationPartialController = new NavigationPartialController(categoryServiceMock.Object);
            var partialViewName = "_NavigationPartial";

            // Act
            var result = navigationPartialController.Index() as PartialViewResult;

            // Assert
            Assert.AreEqual(partialViewName, result.ViewName);
        }

        [Test]
        public void Index_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var navigationPartialController = new NavigationPartialController(categoryServiceMock.Object);
            var attributes = navigationPartialController.GetType().GetMethod("Index").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Index_WhenCalled_ShouldCallGetAllCategoriesOnce()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(It.IsAny<IEnumerable<ICategory>>());
            var navigationPartialController = new NavigationPartialController(categoryServiceMock.Object);

            // Act
            navigationPartialController.Index();

            // Assert
            categoryServiceMock.Verify(x => x.GetAllCategories(), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ShouldSetCorrectCategoriesToViewModel()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var categories = new List<ICategory>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(categories);
            var navigationPartialController = new NavigationPartialController(categoryServiceMock.Object);

            // Act
            var result = navigationPartialController.Index() as PartialViewResult;
            var model = result.Model as NavigationPartialViewModel;

            // Assert
            Assert.AreSame(categories, model.Categories);
        }
    }
}
