using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Seeders;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using System.Data.Entity;

namespace OnTheRoad.Data.Tests.Seeders
{
    [TestFixture]
    public class DataSeederTests
    {
        [Test]
        public void WhenDataReaderPropertyIsCalled_ShouldReturnAnInstanceOfIDataReader()
        {
            var dataSeeder = new DataSeeder();
            var actual = dataSeeder.DataReader;

            Assert.That(actual, Is.InstanceOf<IDataReader>());
        }

        [Test]
        public void WhenDataReaderPropertySetWithNull_ShouldThrowArgumentNullException()
        {
            var dataSeeder = new DataSeeder();

            Assert.Throws<ArgumentNullException>(() => dataSeeder.DataReader = null);
        }

        [Test]
        public void WhenDataReaderPropertySetWithNull_ShouldThrowArgumentNullExceptionWithProperMessage()
        {
            var dataSeeder = new DataSeeder();

            var exc = Assert.Throws<ArgumentNullException>(() => dataSeeder.DataReader = null);
            StringAssert.Contains("dataReader cannot be null!", exc.ParamName);
        }

        [Test]
        public void WhenDataReaderPropertySetWithProperInstance_ShouldSetInstanceOfIDataReader()
        {
            var dataReaderMock = new Mock<IDataReader>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;

            var actual = dataSeeder.DataReader;
            Assert.That(actual, Is.InstanceOf<IDataReader>());
        }

        [Test]
        public void WhenAddOrUpdateHelperPropertyIsCalled_ShouldReturnAnInstanceOfIAddOrUpdateHelper()
        {
            var dataSeeder = new DataSeeder();
            var actual = dataSeeder.AddOrUpdateHelper;

            Assert.That(actual, Is.InstanceOf<IAddOrUpdateHelper>());
        }

        [Test]
        public void WhenAddOrUpdateHelperPropertySetWithNull_ShouldThrowArgumentNullException()
        {
            var dataSeeder = new DataSeeder();

            Assert.Throws<ArgumentNullException>(() => dataSeeder.AddOrUpdateHelper = null);
        }

        [Test]
        public void WhenAddOrUpdateHelperPropertySetWithNull_ShouldThrowArgumentNullExceptionWithProperMessage()
        {
            var dataSeeder = new DataSeeder();

            var exc = Assert.Throws<ArgumentNullException>(() => dataSeeder.AddOrUpdateHelper = null);
            StringAssert.Contains("addOrUpdateHelper cannot be null!", exc.ParamName);
        }

        [Test]
        public void WhenAddOrUpdateHelperSetWithProperInstance_ShouldSetInstanceOfIAddOrUpdateHelper()
        {
            var addOrUpdateHelperMock = new Mock<IAddOrUpdateHelper>();

            var dataSeeder = new DataSeeder();
            dataSeeder.AddOrUpdateHelper = addOrUpdateHelperMock.Object;

            var actual = dataSeeder.AddOrUpdateHelper;
            Assert.That(actual, Is.InstanceOf<IAddOrUpdateHelper>());
        }

        [Test]
        public void DataReader_WhenSeedCategoriesIsCalled_ShouldCallReadCategoriesExactlyOnce()
        {
            var dataReaderMock = new Mock<IDataReader>();
            var contextMock = new Mock<IOnTheRoadDbContext>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;
            dataSeeder.SeedCategories(contextMock.Object);

            dataReaderMock.Verify(x => x.ReadCategories(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(34)]
        [TestCase(0)]
        [Test]
        public void AddOrUpdateHelper_WhenSeedCategoriesIsCalled_ShouldCallAddOrUpdateEntity(int categoriesCount)
        {
            var contextMock = new Mock<IOnTheRoadDbContext>();
            var categories = new List<string>();
            for (int i = 0; i < categoriesCount; i++)
            {
                categories.Add("notImportant" + i);
            }

            var dataReaderMock = new Mock<IDataReader>();
            dataReaderMock.Setup(x => x.ReadCategories()).Returns(categories);
            var addOrUpdateMock = new Mock<IAddOrUpdateHelper>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;
            dataSeeder.AddOrUpdateHelper = addOrUpdateMock.Object;
            dataSeeder.SeedCategories(contextMock.Object);

            addOrUpdateMock.Verify(x => x.AddOrUpdateEntity(It.IsAny<IOnTheRoadDbContext>(), It.IsAny<Category>()), Times.Exactly(categoriesCount));
        }

        [Test]
        public void DataReader_WhenSeedCitiesIsCalled_ShouldCallReadCitiesExactlyOnce()
        {
            var dataReaderMock = new Mock<IDataReader>();
            var contextMock = new Mock<IOnTheRoadDbContext>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;
            dataSeeder.SeedCities(contextMock.Object);

            dataReaderMock.Verify(x => x.ReadCities(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(34)]
        [TestCase(0)]
        [Test]
        public void AddOrUpdateHelper_WhenSeedCitiesIsCalled_ShouldCallAddOrUpdateEntity(int citiesCount)
        {
            var contextMock = new Mock<IOnTheRoadDbContext>();
            var cities = new List<string>();
            for (int i = 0; i < citiesCount; i++)
            {
                cities.Add("notImportant" + i);
            }

            var dataReaderMock = new Mock<IDataReader>();
            dataReaderMock.Setup(x => x.ReadCities()).Returns(cities);
            var addOrUpdateMock = new Mock<IAddOrUpdateHelper>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;
            dataSeeder.AddOrUpdateHelper = addOrUpdateMock.Object;
            dataSeeder.SeedCities(contextMock.Object);

            addOrUpdateMock.Verify(x => x.AddOrUpdateEntity(It.IsAny<IOnTheRoadDbContext>(), It.IsAny<City>()), Times.Exactly(citiesCount));
        }

        [Test]
        public void DataReader_WhenSeedRatingssIsCalled_ShouldCallReadRatingsExactlyOnce()
        {
            var dataReaderMock = new Mock<IDataReader>();
            var contextMock = new Mock<IOnTheRoadDbContext>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;
            dataSeeder.SeedRating(contextMock.Object);

            dataReaderMock.Verify(x => x.ReadRatings(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(34)]
        [TestCase(0)]
        [Test]
        public void AddOrUpdateHelper_WhenSeedRatingsIsCalled_ShouldCallAddOrUpdateEntity(int ratingsCount)
        {
            var contextMock = new Mock<IOnTheRoadDbContext>();
            var ratings = new List<string>();
            for (int i = 0; i < ratingsCount; i++)
            {
                ratings.Add("notImportant" + i);
            }

            var dataReaderMock = new Mock<IDataReader>();
            dataReaderMock.Setup(x => x.ReadRatings()).Returns(ratings);
            var addOrUpdateMock = new Mock<IAddOrUpdateHelper>();

            var dataSeeder = new DataSeeder();
            dataSeeder.DataReader = dataReaderMock.Object;
            dataSeeder.AddOrUpdateHelper = addOrUpdateMock.Object;
            dataSeeder.SeedRating(contextMock.Object);

            addOrUpdateMock.Verify(x => x.AddOrUpdateEntity(It.IsAny<IOnTheRoadDbContext>(), It.IsAny<Rating>()), Times.Exactly(ratingsCount));
        }
    }
}
