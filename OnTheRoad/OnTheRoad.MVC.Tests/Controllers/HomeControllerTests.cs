using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OnTheRoad.MVC.Controllers;
using NUnit.Framework;
using Moq;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.MVC.Models;
using AutoMapper;
using OnTheRoad.MVC.Common;
using System.Linq;

namespace OnTheRoad.MVC.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Controller_WhenTripServiceIsNull_ShouldThrow()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(null, userServiceMock.Object));
        }

        [Test]
        public void Controller_WhenUserServiceIsNull_ShouldThrow()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(tripServiceMock.Object, null));
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnViewResultInstance()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var userServiceMock = new Mock<IUserService>();
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            var result = homeController.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnDefaultView()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var userServiceMock = new Mock<IUserService>();
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            var result = homeController.Index() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void Index_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var userServiceMock = new Mock<IUserService>();
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);
            var attributes = homeController.GetType().GetMethod("Index").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Index_WhenCalled_ShouldCallGetTripsOnceWithCorrectParameters()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var skip = 0;
            var take = 4;
            var trips = new List<ITrip>();
            tripServiceMock.Setup(x => x.GetTrips(It.Is<int>(o => o == skip), It.Is<int>(o => o == take))).Returns(trips);
            var userServiceMock = new Mock<IUserService>();
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            homeController.Index();

            // Assert
            tripServiceMock.Verify(x => x.GetTrips(It.Is<int>(o => o == skip), It.Is<int>(o => o == take)), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ShouldCallGetAllUsersCountOnce()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetAllUsersCount()).Returns(It.IsAny<int>());
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            homeController.Index();

            // Assert
            userServiceMock.Verify(x => x.GetAllUsersCount(), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ShouldCallGetTripsCountOnce()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            tripServiceMock.Setup(x => x.GetTripsCount()).Returns(It.IsAny<int>());
            var userServiceMock = new Mock<IUserService>();
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            homeController.Index();

            // Assert
            tripServiceMock.Verify(x => x.GetTripsCount(), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ShouldSetCorrectTripsToViewModel()
        {
            // Arrange
            var tripViewModelMock = new Mock<TripViewModel>();
            var autoMapperMock = new Mock<IMapper>();
            autoMapperMock.Setup(x => x.Map<TripViewModel>(It.IsAny<ITrip>())).Returns(tripViewModelMock.Object);
            MapperProvider.Mapper = autoMapperMock.Object;

            var tripServiceMock = new Mock<ITripGetService>();
            var tripMock = new Mock<ITrip>();
            var trips = new List<ITrip>() { tripMock.Object };
            tripServiceMock.Setup(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>())).Returns(trips);

            var userServiceMock = new Mock<IUserService>();

            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            var result = homeController.Index() as ViewResult;
            var model = result.Model as HomeViewModel;

            // Assert
            Assert.AreSame(tripViewModelMock.Object, (model.Trips as List<TripViewModel>)[0]);
        }

        [Test]
        public void Index_WhenCalled_ShouldSetCorrectAllUsersCountToViewModel()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var userServiceMock = new Mock<IUserService>();
            var usersCount = 5;
            userServiceMock.Setup(x => x.GetAllUsersCount()).Returns(usersCount);
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            var result = homeController.Index() as ViewResult;
            var model = result.Model as HomeViewModel;

            // Assert
            Assert.AreEqual(usersCount, model.AllUsersCount);
        }

        [Test]
        public void Index_WhenCalled_ShouldSetCorrectAllTripsCountToViewModel()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripGetService>();
            var tripsCount = 5;
            tripServiceMock.Setup(x => x.GetTripsCount()).Returns(tripsCount);
            var userServiceMock = new Mock<IUserService>();
            var homeController = new HomeController(tripServiceMock.Object, userServiceMock.Object);

            // Act
            var result = homeController.Index() as ViewResult;
            var model = result.Model as HomeViewModel;

            // Assert
            Assert.AreEqual(tripsCount, model.AllTripsCount);
        }
    }
}
