using AutoMapper;
using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Controllers;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Tests.Controllers
{
    [TestFixture]
    public class CategoryOverviewPartialControllerTests
    {
        [Test]
        public void Constructor_WhenTripGetServiceIsNull_ShouldThrow()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoryOverviewPartialController(null));
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnPartialViewResultInstance()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var categoryOverviewPartialController = new CategoryOverviewPartialController(tripServiceMock.Object);
            string categoryName = "Category Name";

            // Act
            var result = categoryOverviewPartialController.Index(categoryName);

            // Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnCorrectPartialView()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var categoryOverviewPartialController = new CategoryOverviewPartialController(tripServiceMock.Object);
            string categoryName = "Category Name";

            // Act
            var result = categoryOverviewPartialController.Index(categoryName) as PartialViewResult;

            // Assert
            Assert.AreEqual("_CategoryOverviewPartial", result.ViewName);
        }

        [Test]
        public void Index_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var categoryOverviewPartialController = new CategoryOverviewPartialController(tripServiceMock.Object);
            var attributes = categoryOverviewPartialController.GetType().GetMethod("Index").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnCallGetTripsByCategoryNameOrderedByDateOnce()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var categoryOverviewPartialController = new CategoryOverviewPartialController(tripServiceMock.Object);
            var tripsCount = 4;
            string categoryName = "Category Name";

            // Act
            categoryOverviewPartialController.Index(categoryName);

            // Assert
            tripServiceMock.Verify(x => x.GetTripsByCategoryNameOrderedByDate(It.Is<string>(o => o == categoryName), It.Is<int>(o => o == tripsCount), false), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ShouldSetCorretTripsToModel()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var tripMock = new Mock<ITrip>();
            var trips = new List<ITrip>() { tripMock.Object };
            tripServiceMock.Setup(x => x.GetTripsByCategoryNameOrderedByDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(trips);

            var tripModelMock = new Mock<TripViewModel>();
            var autoMapperMock = new Mock<IMapper>();
            autoMapperMock.Setup(x => x.Map<TripViewModel>(It.IsAny<ITrip>())).Returns(tripModelMock.Object);
            MapperProvider.Mapper = autoMapperMock.Object;

            var categoryOverviewPartialController = new CategoryOverviewPartialController(tripServiceMock.Object);
            string categoryName = "Category Name";

            // Act
            var result = categoryOverviewPartialController.Index(categoryName) as PartialViewResult;
            var model = result.Model as CategoryOverviewViewModel;

            // Assert
            Assert.AreSame(tripModelMock.Object, (model.Trips as List<TripViewModel>)[0]);
        }

        [Test]
        public void Index_WhenCalled_ShouldSetCorretNameToModel()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var categoryOverviewPartialController = new CategoryOverviewPartialController(tripServiceMock.Object);
            string categoryName = "Category Name";

            // Act
            var result = categoryOverviewPartialController.Index(categoryName) as PartialViewResult;
            var model = result.Model as CategoryOverviewViewModel;

            // Assert
            Assert.AreSame(categoryName, model.Name);
        }
    }
}
