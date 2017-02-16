using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Builders;

namespace OnTheRoad.Logic.Tests.Builders
{
    [TestFixture]
    public class TripBuilderTests
    {
        [Test]
        public void Build_WhenCalled_ShouldCreateNewTripInstance()
        {
            var builder = new TripBuilder();

            var trip = builder.Build();

            Assert.IsInstanceOf<ITrip>(trip);
        }

        [Test]
        public void Build_WhenCalled_ShouldReturnNotNull()
        {
            var builder = new TripBuilder();

            var trip = builder.Build();

            Assert.IsNotNull(trip);
        }

        [Test]
        public void SetCategories_WhenCalled_ShouldSetTripCategoriesProperty()
        {
            var builder = new TripBuilder();
            var categoryMock = new Mock<ICategory>();
            var categories = new List<ICategory>() { categoryMock.Object };

            builder.SetCategories(categories);
            var trip = builder.Build();

            Assert.AreSame(categories, trip.Categories);
        }

        [Test]
        public void SetCategories_WhenCalledWithNull_ShouldSetTripCategoriesPropertyWithNull()
        {
            var builder = new TripBuilder();

            builder.SetCategories(null);
            var trip = builder.Build();

            Assert.IsNull(trip.Categories);
        }

        [TestCase("description")]
        [TestCase("another description")]
        public void SetDescription_WhenCalled_ShouldSetTripDescriptionProperty(string description)
        {
            var builder = new TripBuilder();

            builder.SetDescription(description);
            var trip = builder.Build();

            Assert.AreSame(description, trip.Description);
        }

        [Test]
        public void SetDescription_WhenCalledWithNull_ShouldSetTripDescriptionPropertyWithNull()
        {
            var builder = new TripBuilder();

            builder.SetDescription(null);
            var trip = builder.Build();

            Assert.IsNull(trip.Description);
        }

        [Test]
        public void SetEndDate_WhenCalled_ShouldSetTripEndDateProperty()
        {
            var builder = new TripBuilder();
            var endDate = DateTime.Now;

            builder.SetEndDate(endDate);
            var trip = builder.Build();

            Assert.That(trip.EndDate.CompareTo(endDate) == 0);
        }

        [Test]
        public void SetStartDate_WhenCalled_ShouldSetTripEndDateProperty()
        {
            var builder = new TripBuilder();
            var startDate = DateTime.Now;

            builder.SetStartDate(startDate);
            var trip = builder.Build();

            Assert.That(trip.StartDate.CompareTo(startDate) == 0);
        }

        [Test]
        public void SetImage_WhenCalled_ShouldSetTripImageProperty()
        {
            var builder = new TripBuilder();
            var imageMock = new Mock<IImage>();

            builder.SetImage(imageMock.Object);
            var trip = builder.Build();

            Assert.AreSame(imageMock.Object, trip.CoverImage);
        }

        [Test]
        public void SetImage_WhenCalledWithNull_ShouldSetTripImagePropertyWithNull()
        {
            var builder = new TripBuilder();

            builder.SetImage(null);
            var trip = builder.Build();

            Assert.IsNull(trip.CoverImage);
        }

        [TestCase("location")]
        [TestCase("another location")]
        public void SetLocation_WhenCalled_ShouldSetTripLocationProperty(string location)
        {
            var builder = new TripBuilder();

            builder.SetLocation(location);
            var trip = builder.Build();

            Assert.AreSame(location, trip.Location);
        }

        [Test]
        public void SetLocation_WhenCalledWithNull_ShouldSetTripLocationPropertyWithNull()
        {
            var builder = new TripBuilder();

            builder.SetLocation(null);
            var trip = builder.Build();

            Assert.IsNull(trip.Location);
        }

        [TestCase("name")]
        [TestCase("another name")]
        public void SetName_WhenCalled_ShouldSetTripNameProperty(string name)
        {
            var builder = new TripBuilder();

            builder.SetName(name);
            var trip = builder.Build();

            Assert.AreSame(name, trip.Name);
        }

        [Test]
        public void SetName_WhenCalledWithNull_ShouldSetTripNamePropertyWithNull()
        {
            var builder = new TripBuilder();

            builder.SetName(null);
            var trip = builder.Build();

            Assert.IsNull(trip.Name);
        }

        [Test]
        public void SetTags_WhenCalled_ShouldSetTripTagsProperty()
        {
            var builder = new TripBuilder();
            var tagMock = new Mock<ITag>();
            var tags = new List<ITag>() { tagMock.Object };

            builder.SetTags(tags);
            var trip = builder.Build();

            Assert.AreSame(tags, trip.Tags);
        }

        [Test]
        public void SetTags_WhenCalledWithNull_ShouldSetTripTagsPropertyWithNull()
        {
            var builder = new TripBuilder();

            builder.SetTags(null);
            var trip = builder.Build();

            Assert.IsNull(trip.Tags);
        }
    }
}
