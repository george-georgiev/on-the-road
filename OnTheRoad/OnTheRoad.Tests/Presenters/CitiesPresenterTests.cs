using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class CitiesPresenterTests
    {
        [Test]
        public void CitiesPresenter_WhenInitializedWithNullForICityService_ShouldThrowArgumentNullException()
        {
            var citiesViewMock = new Mock<ICitiesView>();

            Assert.Throws<ArgumentNullException>(() => new CitiesPresenter(citiesViewMock.Object, null));
        }

        [Test]
        public void CitiesPresenter_WhenInitializedWithNullForICityService_ShouldThrowArgumentNullExceptionWithProperException()
        {
            var citiesViewMock = new Mock<ICitiesView>();

            var exc = Assert.Throws<ArgumentNullException>(() => new CitiesPresenter(citiesViewMock.Object, null));
            StringAssert.Contains("cityService cannot be null!", exc.Message);
        }

        [Test]
        public void CityService_WhenGetCitiesIsRaised_ShouldCallGetAllCitiesExactlyOnce()
        {
            var citiesViewMock = new Mock<ICitiesView>();
            var cityServiceMock = new Mock<ICityService>();
            var citiesModelMock = new Mock<CitiesModel>();
            citiesViewMock.Setup(x => x.Model).Returns(citiesModelMock.Object);

            var citiesPresenter = new CitiesPresenter(citiesViewMock.Object, cityServiceMock.Object);
            citiesViewMock.Raise(x => x.GetCities += null, null, new EventArgs());

            cityServiceMock.Verify(x => x.GetAllCities(), Times.Once);
        }

        [Test]
        public void CitiesModelPropertyCities_WhenGetCitiesIsRaised_ShouldBeIEnumerableFromCities()
        {
            var citiesViewMock = new Mock<ICitiesView>();
            var cityServiceMock = new Mock<ICityService>();
            var citiesModelMock = new Mock<CitiesModel>();
            citiesViewMock.Setup(x => x.Model).Returns(citiesModelMock.Object);
            cityServiceMock.Setup(x => x.GetAllCities()).Returns(new List<ICity>());

            var citiesPresenter = new CitiesPresenter(citiesViewMock.Object, cityServiceMock.Object);
            citiesViewMock.Raise(x => x.GetCities += null, null, new EventArgs());

            Assert.That(citiesModelMock.Object.Cities, Is.InstanceOf<IEnumerable<ICity>>());
        }
    }
}