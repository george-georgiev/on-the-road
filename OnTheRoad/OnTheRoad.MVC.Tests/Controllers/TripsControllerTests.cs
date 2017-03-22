using AutoMapper;
using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.Infrastructure.Json;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Contracts;
using OnTheRoad.MVC.Controllers;
using OnTheRoad.MVC.Filters;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Tests.Controllers
{
    [TestFixture]
    public class TripsControllerTests
    {
        [SetUp]
        public void Setup()
        {
            var autoMapperMock = new Mock<IMapper>();
            autoMapperMock.Setup(x => x.Map<TripDetailsViewModel>(It.IsAny<ITrip>())).Returns((new Mock<TripDetailsViewModel>()).Object);
            MapperProvider.Mapper = autoMapperMock.Object;
        }

        [TearDown]
        public void TearDown()
        {
            MapperProvider.Mapper = null;
        }

        [Test]
        public void Constructor_WhenTripServiceIsNull_ShouldThrow()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TripsController(
                null,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object));
        }

        [Test]
        public void Constructor_WhenSubscriptionServiceIsNull_ShouldThrow()
        {
            // Arrange
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TripsController(
                tripServiceMock.Object,
                null,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object));
        }

        [Test]
        public void Constructor_WhenCategoryServiceIsNull_ShouldThrow()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                null,
                imageServiceMock.Object, tripBuilderMock.Object));
        }

        [Test]
        public void Constructor_WhenImageServiceIsNull_ShouldThrow()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                null,
                tripBuilderMock.Object));
        }

        [Test]
        public void Constructor_WhenTripBuilderIsNull_ShouldThrow()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                null));
        }

        [Test]
        public void Search_ShouldReturnViewResultInstance()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.Search(It.IsAny<string>());

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Search_ShouldReturnCorrectView()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.Search(It.IsAny<string>()) as ViewResult;

            // Assert
            Assert.AreEqual("_TripsWithPaging", result.ViewName);
        }

        [Test]
        public void Search_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("Search").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [TestCase(5)]
        [TestCase(0)]
        [TestCase(-5)]
        public void Search_WhenPatternIsNotNullEmptyOrWhitespace_ShouldCallGetTripsBySearchPatternOnceWithCorrectParameters(int page)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";
            var take = 3;

            // Act
            tripsController.Search(pattern, page);

            // Assert
            page = page > 0 ? page : 1;
            var skip = (page - 1) * take;
            tripServiceMock.Verify(x => x.GetTripsBySearchPattern(It.Is<string>(o => o == pattern), It.Is<int>(o => o == skip), It.Is<int>(o => o == take)), Times.Once);
        }

        [Test]
        public void Search_WhenPatternIsNotNullEmptyOrWhitespace_ShouldCallGetTripsCountBySearchPatternOnceWithCorrectParameters()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";
            var page = 5;

            // Act
            tripsController.Search(pattern, page);

            // Assert
            tripServiceMock.Verify(x => x.GetTripsCountBySearchPattern(It.Is<string>(o => o == pattern)), Times.Once);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("         ")]
        public void Search_WhenPatternIsNullEmptyOrWhitespace_ShouldCallGetTripsBySearchPatternNever(string pattern)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var page = 5;

            // Act
            tripsController.Search(pattern, page);

            // Assert
            tripServiceMock.Verify(x => x.GetTripsBySearchPattern(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("         ")]
        public void Search_WhenPatternIsNullEmptyOrWhitespace_ShouldCallGetTripsCountBySearchPatternNever(string pattern)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var autoMapperMock = new Mock<IMapper>();
            MapperProvider.Mapper = autoMapperMock.Object;

            var page = 5;

            // Act
            tripsController.Search(pattern, page);

            // Assert
            tripServiceMock.Verify(x => x.GetTripsCountBySearchPattern(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Search_ShouldSetCorrectHeadingPropertyToModel()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";

            // Act
            var result = tripsController.Search(pattern) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            Assert.AreEqual($"{Resources.Labels.SearchResultsFor}: {pattern}", model.Heading);
        }

        [Test]
        public void Search_ShouldSetCorrectPageHyperLinkPropertyToModel()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";

            // Act
            var result = tripsController.Search(pattern) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            Assert.AreEqual($"/trips/search/{pattern}/", model.PageHyperLink);
        }

        [TestCase(5)]
        [TestCase(0)]
        [TestCase(-5)]
        public void Search_WhenPatternIsNotNullEmptyOrWhiteSpace_ShouldSetCorrectPagePropertyToModel(int page)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";

            // Act
            var result = tripsController.Search(pattern, page) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            var expected = page > 0 ? page : 1;
            Assert.AreEqual(expected, model.Page);
        }

        [TestCase(5, null)]
        [TestCase(0, "")]
        [TestCase(-5, "           ")]
        public void Search_WhenPatternIsNullEmptyOrWhiteSpace_ShouldSetPagePropertyToOne(int page, string pattern)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.Search(pattern, page) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            var expected = 1;
            Assert.AreEqual(expected, model.Page);
        }

        [Test]
        public void Search_WhenPatternIsNotNullEmptyOrWhiteSpace_ShouldSetCorrectTripsPropertyToModel()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var trips = new List<ITrip>() { tripMock.Object };
            tripServiceMock.Setup(x => x.GetTripsBySearchPattern(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(trips);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";

            var tripModelMock = new Mock<TripViewModel>();
            var tripModels = new List<TripViewModel>() { tripModelMock.Object };

            var autoMapperMock = new Mock<IMapper>();
            autoMapperMock.Setup(x => x.Map<IEnumerable<TripViewModel>>(It.Is<IEnumerable<ITrip>>(o => o.Equals(trips)))).Returns(tripModels);
            MapperProvider.Mapper = autoMapperMock.Object;

            // Act
            var result = tripsController.Search(pattern) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            Assert.AreSame(tripModels, model.Trips);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("           ")]
        public void Search_WhenPatternIsNullEmptyOrWhiteSpace_ShouldSetTripsPropertyToEmptyCollection(string pattern)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.Search(pattern) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            Assert.AreEqual(0, model.Trips.Count());
        }

        [Test]
        public void Search_WhenPatternIsNotNullEmptyOrWhiteSpace_ShouldSetCorrectTotalProperty()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var total = 5;
            tripServiceMock.Setup(x => x.GetTripsCountBySearchPattern(It.IsAny<string>())).Returns(total);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";

            // Act
            var result = tripsController.Search(pattern) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            Assert.AreEqual(total, model.Total);
        }

        [Test]
        public void Search_WhenPatternIsNotNullEmptyOrWhiteSpace_ShouldSetCorrectTakeProperty()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var pattern = "Pattern";
            var take = 3;

            // Act
            var result = tripsController.Search(pattern) as ViewResult;
            var model = result.Model as TripsWithPagingViewModel;

            // Assert
            Assert.AreEqual(take, model.Take);
        }

        [Test]
        public void Details_ShouldReturnViewResultInstance()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>());

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Details_ShouldReturnDefaultView()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void Details_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("Details").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Details_ShouldCallGetTripByIdOnceWithCorrectParameters()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;

            // Act
            tripsController.Details(tripId);

            // Assert
            tripServiceMock.Verify(x => x.GetTripById(It.Is<int>(o => o == tripId)), Times.Once);
        }

        [Test]
        public void Details_WhenNotLoggedIn_ShouldSetCanSubscribePropertyToFalse()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            controllerUtilMock.Setup(x => x.IsAuthenticated).Returns(false);
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;
            var model = result.Model as TripDetailsViewModel;

            // Assert
            Assert.False(model.CanSubscribe);
        }

        [Test]
        public void Details_WhenIsOrganiser_ShouldSetCanSubscribePropertyToFalse()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            var username = "username";
            organiserMock.Setup(x => x.Username).Returns(username);
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            controllerUtilMock.Setup(x => x.IsAuthenticated).Returns(true);
            controllerUtilMock.Setup(x => x.LoggedUserName).Returns(username);
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;
            var model = result.Model as TripDetailsViewModel;

            // Assert
            Assert.False(model.CanSubscribe);
        }

        [Test]
        public void Details_WhenIsNotOrganiser_ShouldSetCanSubscribePropertyToTrue()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            organiserMock.Setup(x => x.Username).Returns("username1");
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            controllerUtilMock.Setup(x => x.IsAuthenticated).Returns(true);
            controllerUtilMock.Setup(x => x.LoggedUserName).Returns("username2");
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;
            var model = result.Model as TripDetailsViewModel;

            // Assert
            Assert.True(model.CanSubscribe);
        }

        [TestCase(SubscriptionStatus.None)]
        [TestCase(SubscriptionStatus.Attending)]
        public void Details_SetCorrectIsNonePropertyToModel(SubscriptionStatus subscriptionStatus)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(x => x.GetUserSubscriptionStatus(It.IsAny<ITrip>(), It.IsAny<string>())).Returns(subscriptionStatus);

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;
            var model = result.Model as TripDetailsViewModel;

            // Assert
            var expected = subscriptionStatus == SubscriptionStatus.None;
            Assert.AreEqual(expected, model.IsNone);
        }

        [TestCase(SubscriptionStatus.None)]
        [TestCase(SubscriptionStatus.Attending)]
        public void Details_SetCorrectIsAttendingPropertyToModel(SubscriptionStatus subscriptionStatus)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(x => x.GetUserSubscriptionStatus(It.IsAny<ITrip>(), It.IsAny<string>())).Returns(subscriptionStatus);

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;
            var model = result.Model as TripDetailsViewModel;

            // Assert
            var expected = subscriptionStatus == SubscriptionStatus.Attending;
            Assert.AreEqual(expected, model.IsAttending);
        }

        [TestCase(SubscriptionStatus.None)]
        [TestCase(SubscriptionStatus.Attending)]
        public void Details_SetCorrectIsIsInterestedPropertyToModel(SubscriptionStatus subscriptionStatus)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(x => x.GetUserSubscriptionStatus(It.IsAny<ITrip>(), It.IsAny<string>())).Returns(subscriptionStatus);

            var tripServiceMock = new Mock<ITripService>();
            var tripMock = new Mock<ITrip>();
            var organiserMock = new Mock<IUser>();
            tripMock.Setup(x => x.Organiser).Returns(organiserMock.Object);
            tripServiceMock.Setup(x => x.GetTripById(It.IsAny<int>())).Returns(tripMock.Object);

            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            // Act
            var result = tripsController.Details(It.IsAny<int>()) as ViewResult;
            var model = result.Model as TripDetailsViewModel;

            // Assert
            var expected = subscriptionStatus == SubscriptionStatus.Interested;
            Assert.AreEqual(expected, model.IsInterested);
        }

        [Test]
        public void Subscribe_ShouldReturnJsonResultInstance()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            var result = tripsController.Subscribe(It.IsAny<int>(), statusValue);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void Subscribe_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("Subscribe").GetCustomAttributes(typeof(HttpPostAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Subscribe_ShouldBeDecoratedWithAuthorizeAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("Subscribe").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Subscribe_ShouldBeDecoratedWithAjaxAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("Subscribe").GetCustomAttributes(typeof(AjaxAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Subscribe_ShouldCallAddOrUpdateSubscriptionOnceWithCorrectParameters()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            var username = "username";
            controllerUtilMock.Setup(x => x.LoggedUserName).Returns(username);
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;
            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            tripsController.Subscribe(tripId, statusValue);

            // Assert
            subscriptionServiceMock.Verify(
                x => x.AddOrUpdateSubscription(
                    It.Is<string>(o => o == username),
                    It.Is<int>(o => o == tripId),
                    It.Is<SubscriptionStatus>(o => o == SubscriptionStatus.Interested)),
                Times.Once);
        }

        [Test]
        public void Subscribe_WhenIsSuccessful_ShouldReturnJsonResultWithSuccessDisplayMessage()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;
            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            var result = tripsController.Subscribe(tripId, statusValue) as JsonResult;
            var data = result.Data as Result;

            // Assert
            Assert.AreEqual(Resources.Messages.SubscriptionSuccess, data.DisplayMessage);
        }

        [Test]
        public void Subscribe_WhenIsSuccessful_ShouldReturnJsonResultWithStatusOk()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;
            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            var result = tripsController.Subscribe(tripId, statusValue) as JsonResult;
            var data = result.Data as Result;

            // Assert
            Assert.AreEqual(ResponseStatus.Ok, data.Status);
        }

        [Test]
        public void Subscribe_WhenIsNotSuccessful_ShouldReturnJsonResultWithErrorDisplayMessage()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(
                x => x.AddOrUpdateSubscription(
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<SubscriptionStatus>()))
                    .Throws<Exception>();

            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;
            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            var result = tripsController.Subscribe(tripId, statusValue) as JsonResult;
            var data = result.Data as Result;

            // Assert
            Assert.AreEqual(Resources.Messages.SubscriptionError, data.DisplayMessage);
        }

        [Test]
        public void Subscribe_WhenIsNotSuccessful_ShouldReturnJsonResultWithStatusServerError()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(
                x => x.AddOrUpdateSubscription(
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<SubscriptionStatus>()))
                    .Throws<Exception>();

            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;
            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            var result = tripsController.Subscribe(tripId, statusValue) as JsonResult;
            var data = result.Data as Result;

            // Assert
            Assert.AreEqual(ResponseStatus.ServerError, data.Status);
        }

        [Test]
        public void Subscribe_WhenIsNotSuccessful_ShouldSetCorrectResponseStatus()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(
                x => x.AddOrUpdateSubscription(
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<SubscriptionStatus>()))
                    .Throws<Exception>();

            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var controllerUtilMock = new Mock<IControllerUtil>();
            ControllerUtilProvider.ControllerUtil = controllerUtilMock.Object;

            var tripId = 5;
            var statusValue = SubscriptionStatus.Interested.ToString();

            // Act
            tripsController.Subscribe(tripId, statusValue);

            // Assert
            controllerUtilMock.Verify(x => x.SetResponseStatusCode(It.Is<ResponseStatus>(o => o == ResponseStatus.ServerError)), Times.Once);
        }

        [Test]
        public void AddTripGet_ShouldReturnViewResultInstance()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<ICategory>());
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.AddTrip();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void AddTripGet_ShouldReturnDefaultView()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<ICategory>());
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.AddTrip() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void AddTripGet_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("AddTrip", new Type[0]).GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void AddTripGet_ShouldBeDecoratedWithAuthorizeAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("AddTrip", new Type[0]).GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void AddTripGet_ShouldCallGetAllCategoriesOnce()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<ICategory>());
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            tripsController.AddTrip();

            // Assert
            categoryServiceMock.Verify(x => x.GetAllCategories(), Times.Once);
        }

        [Test]
        public void AddTripGet_ShouldSetCorrectAllCategoriesPropertyToModel()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();

            var categoryServiceMock = new Mock<ICategoryService>();
            var categoryMock = new Mock<ICategory>();
            var id = 5;
            var name = "Name";
            categoryMock.Setup(x => x.Id).Returns(id);
            categoryMock.Setup(x => x.Name).Returns(name);
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<ICategory>() { categoryMock.Object });

            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.AddTrip() as ViewResult;
            var model = result.Model as AddTripViewModel;
            var allCategories = model.AllCategories as List<SelectListItem>;

            // Assert
            Assert.AreEqual(id, int.Parse(allCategories[0].Value));
            Assert.AreEqual(name, allCategories[0].Text);
            Assert.AreEqual(1, allCategories.Count);
        }

        [Test]
        public void AddTripGet_ShouldCallLoadTripsImageOnce()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<ICategory>());
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            tripsController.AddTrip();

            // Assert
            imageServiceMock.Verify(x => x.LoadTripsImage(), Times.Once);
        }

        [Test]
        public void AddTripGet_ShouldSetCorrectCoverImagePropertyToModel()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var imageServiceMock = new Mock<IImageService>();
            var image = new byte[5];
            imageServiceMock.Setup(x => x.LoadTripsImage()).Returns(image);

            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            // Act
            var result = tripsController.AddTrip() as ViewResult;
            var model = result.Model as AddTripViewModel;

            // Assert
            Assert.AreEqual(image, model.CoverImage);
        }

        [Test]
        public void AddTripPost_ShouldBeDecoratedWithHttpPostAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("AddTrip", new Type[] { typeof(AddTripViewModel) }).GetCustomAttributes(typeof(HttpPostAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void AddTripPost_ShouldBeDecoratedWithAuthorizeAttribute()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            var tripServiceMock = new Mock<ITripService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var tripsController = new TripsController(
                tripServiceMock.Object,
                subscriptionServiceMock.Object,
                categoryServiceMock.Object,
                imageServiceMock.Object,
                tripBuilderMock.Object);

            var attributes = tripsController.GetType().GetMethod("AddTrip", new Type[] { typeof(AddTripViewModel) }).GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }
    }
}
