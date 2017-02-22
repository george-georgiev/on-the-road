using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using OnTheRoad.Mvp.Presenters;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class AddTripPresenterTests
    {
        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForTripAddService_ArgumentNullException_ShouldBeThrown()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            var imageServiceMock = new Mock<IImageService>();

            Assert.Throws<ArgumentNullException>(() => new AddTripPresenter(viewMock.Object, null, tripBuilderMock.Object, imageServiceMock.Object));
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForCategoryService_ArgumentNullException_ShouldBeThrownWithProperMessage()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            var imageServiceMock = new Mock<IImageService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new AddTripPresenter(viewMock.Object, null, tripBuilderMock.Object, imageServiceMock.Object));

            StringAssert.Contains("tripAddService cannot be null!", exc.Message);
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForTripBuilder_ArgumentNullException_ShouldBeThrown()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var imageServiceMock = new Mock<IImageService>();

            Assert.Throws<ArgumentNullException>(() => new AddTripPresenter(viewMock.Object, tripsAddService.Object, null, imageServiceMock.Object));
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForTripBuilder_ArgumentNullException_ShouldBeThrownWithProperMessage()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var imageServiceMock = new Mock<IImageService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new AddTripPresenter(viewMock.Object, tripsAddService.Object, null, imageServiceMock.Object));

            StringAssert.Contains("tripBuilder cannot be null!", exc.Message);
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForImageService_ArgumentNullException_ShouldBeThrown()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            Assert.Throws<ArgumentNullException>(() => new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, null));
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullFormageService_ArgumentNullException_ShouldBeThrownWithProperMessage()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var tripBuilderMock = new Mock<ITripBuilder>();

            var exc = Assert.Throws<ArgumentNullException>(() => new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, null));

            StringAssert.Contains("imageService cannot be null!", exc.Message);
        }

        [Test]
        public void ImageService_WhenGetTripsDefaultImageIsCalled_ShouldCallLoadResizedTripsImageExactlyOnce()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            var imageServiceMock = new Mock<IImageService>();
            var modelMock = new Mock<TripModel>();
            viewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, imageServiceMock.Object);
            viewMock.Raise(x => x.GetTripsDefaultImage += null, null, new EventArgs());

            imageServiceMock.Verify(x => x.LoadTripsImage(), Times.Once);
        }

        [Test]
        public void ViewModel_WhenGetTripsDefaultImageIsCalled_ShouldSetImageContentToTheResultFromLoadResizedTripsImage()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            var imageServiceMock = new Mock<IImageService>();
            var image = new byte[1];
            imageServiceMock.Setup(x => x.LoadTripsImage()).Returns(image);
            var modelMock = new Mock<TripModel>();
            viewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, imageServiceMock.Object);
            viewMock.Raise(x => x.GetTripsDefaultImage += null, null, new EventArgs());

            Assert.That(viewMock.Object.Model.ImageContent.Equals(image));
        }

        [Test]
        public void TripBuilder_WhenCreateTripIsRaised_ShouldCallTheBuildersMethodsExactlyOnce()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            tripBuilderMock.Setup(x => x.SetName(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetDescription(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetLocation(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetStartDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetEndDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetImage(It.IsAny<byte[]>())).Returns(tripBuilderMock.Object);

            var presenter = new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, imageServiceMock.Object);
            viewMock.Raise(x => x.CreateTrip += null, null, new AddTripEventArgs());

            tripBuilderMock.Verify(x => x.SetName(It.IsAny<string>()), Times.Once);
            tripBuilderMock.Verify(x => x.SetDescription(It.IsAny<string>()), Times.Once);
            tripBuilderMock.Verify(x => x.SetLocation(It.IsAny<string>()), Times.Once);
            tripBuilderMock.Verify(x => x.SetStartDate(It.IsAny<DateTime>()), Times.Once);
            tripBuilderMock.Verify(x => x.SetEndDate(It.IsAny<DateTime>()), Times.Once);
            tripBuilderMock.Verify(x => x.SetImage(It.IsAny<byte[]>()), Times.Once);
            tripBuilderMock.Verify(x => x.Build(), Times.Once);
        }

        [Test]
        public void TripBuilder_WhenCreateTripIsRaised_ShouldCallTheBuildersMethodsWithTheParamsGivenFromAddTripEventArgs()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            tripBuilderMock.Setup(x => x.SetName(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetDescription(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetLocation(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetStartDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetEndDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetImage(It.IsAny<byte[]>())).Returns(tripBuilderMock.Object);

            var name = "_";
            var description = "_";
            var location = "_";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;
            var coverImage = new byte[1];

            var presenter = new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, imageServiceMock.Object);
            viewMock.Raise(x => x.CreateTrip += null, null, new AddTripEventArgs()
            {
                TripName = name,
                Description = description,
                Location = location,
                StartDate = startDate,
                EndDate = endDate,
                CoverImageContent = coverImage
            });

            tripBuilderMock.Verify(x => x.SetName(name), Times.Once);
            tripBuilderMock.Verify(x => x.SetDescription(description), Times.Once);
            tripBuilderMock.Verify(x => x.SetLocation(location), Times.Once);
            tripBuilderMock.Verify(x => x.SetStartDate(startDate), Times.Once);
            tripBuilderMock.Verify(x => x.SetEndDate(endDate), Times.Once);
            tripBuilderMock.Verify(x => x.SetImage(coverImage), Times.Once);
            tripBuilderMock.Verify(x => x.Build(), Times.Once);
        }

        [Test]
        public void TripAddService_WhenCreateTripIsRaised_ShouldCallAddTripExactlyOnce()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            tripBuilderMock.Setup(x => x.SetName(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetDescription(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetLocation(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetStartDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetEndDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetImage(It.IsAny<byte[]>())).Returns(tripBuilderMock.Object);

            var presenter = new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, imageServiceMock.Object);
            viewMock.Raise(x => x.CreateTrip += null, null, new AddTripEventArgs());

            tripsAddService.Verify(x => x.AddTrip(It.IsAny<ITrip>(), It.IsAny<string>(), It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<string>>()));
        }

        [Test]
        public void TripAddService_WhenCreateTripIsRaised_ShouldCallAddTripWithTheCorrectParams()
        {
            var viewMock = new Mock<IAddTripView>();
            var tripsAddService = new Mock<ITripAddService>();
            var imageServiceMock = new Mock<IImageService>();
            var tripBuilderMock = new Mock<ITripBuilder>();
            var loggedUsername = "_";
            var categoriesIds = new List<int>();
            var tagName = new List<string>();
            var trip = new Mock<ITrip>();

            tripBuilderMock.Setup(x => x.SetName(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetDescription(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetLocation(It.IsAny<string>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetStartDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetEndDate(It.IsAny<DateTime>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.SetImage(It.IsAny<byte[]>())).Returns(tripBuilderMock.Object);
            tripBuilderMock.Setup(x => x.Build()).Returns(trip.Object);

            var presenter = new AddTripPresenter(viewMock.Object, tripsAddService.Object, tripBuilderMock.Object, imageServiceMock.Object);
            viewMock.Raise(x => x.CreateTrip += null, null, new AddTripEventArgs()
            {
                LoggedUserName = loggedUsername,
                SelectedCategoryIds = categoriesIds,
                SelectedTagNames = tagName
            });

            tripsAddService.Verify(x => x.AddTrip(trip.Object, loggedUsername, categoriesIds, tagName), Times.Once);
        }
    }
}