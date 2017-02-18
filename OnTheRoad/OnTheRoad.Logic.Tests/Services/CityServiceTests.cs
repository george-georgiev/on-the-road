using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Services;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class CityServiceTests
    {
        private Mock<ICityRepository> cityRepositoryMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.cityRepositoryMock = new Mock<ICityRepository>();
        }

        [Test]
        public void CityService_WhenInitializedWithNullForICityRepository_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CityService(null));
        }

        [Test]
        public void CityService_WhenInitializedWithNullForICityRepository_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new CityService(null));
            StringAssert.Contains("cityRepository cannot be null!", exc.Message);
        }

        [Test]
        public void WhenGetAllCitiesIsCalled_ShouldReturnInstanceOfIEnumerableICity()
        {
            var cityService = new CityService(cityRepositoryMock.Object);
            var cities = cityService.GetAllCities();

            Assert.That(cities, Is.InstanceOf<IEnumerable<ICity>>());
        }

        [Test]
        public void WhenGetCityByIdIsCalled_ShouldReturnInstanceOfICity()
        {
            var cityService = new CityService(cityRepositoryMock.Object);
            var cityStub = new Mock<ICity>();
            cityRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(cityStub.Object);
            var city = cityService.GetCityById(1);

            Assert.That(city, Is.InstanceOf<ICity>());
        }
    }
}
