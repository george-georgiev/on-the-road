using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Views;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class CategoriesPresenterTests
    {
        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForCategoryService_ArgumentNullException_ShouldBeThrown()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var tripGetServiceMock = new Mock<ITripGetService>();

            Assert.Throws<ArgumentNullException>(() => new CategoriesPresenter(categoryViewMock.Object, null, tripGetServiceMock.Object));
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForCategoryService_ArgumentNullException_ShouldBeThrownWithProperMessage()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var tripGetServiceMock = new Mock<ITripGetService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new CategoriesPresenter(categoryViewMock.Object, null, tripGetServiceMock.Object));
            StringAssert.Contains("categoryService cannot be null!", exc.Message);
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForTripGetService_ArgumentNullException_ShouldBeThrown()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();

            Assert.Throws<ArgumentNullException>(() => new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, null));
        }

        [Test]
        public void WhenCategoryPresenterIsInitialized_WithNullForTripGetService_ArgumentNullException_ShouldBeThrownWithProperMessage()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, null));
            StringAssert.Contains("tripGetService cannot be null!", exc.Message);
        }

        [Test]
        public void TripGetService_WhenView_GetTripsTotalCountIsRaised_ShouldCallGetTripsCountByCategoryNameExactlyOnce()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetTripsTotalCount += null, null, new CategoriesEventArgs());

            tripGetServiceMock.Verify(x => x.GetTripsCountByCategoryName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TripGetService_WhenView_GetTripsTotalCountIsRaised_ShouldCallGetTripsCountByCategoryNameWithTheCorrectParams()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);
            var name = "_";

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetTripsTotalCount += null, null, new CategoriesEventArgs() { CategoryName = name });

            tripGetServiceMock.Verify(x => x.GetTripsCountByCategoryName(name), Times.Once);
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(5)]
        [Test]
        public void ViewModel_WhenGetTripsTotalCountIsRaised_ShouldSetTripsTotalCountToTheAmountReturnedFromGetTripsCountByCategoryName(int totalCount)
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            tripGetServiceMock.Setup(x => x.GetTripsCountByCategoryName(It.IsAny<string>())).Returns(totalCount);
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetTripsTotalCount += null, null, new CategoriesEventArgs());

            Assert.That(categoryViewMock.Object.Model.TripsTotalCount == totalCount);
        }

        [Test]
        public void TripGetService_WhenGetTripsIsCalled_ShouldCalleGetTripsByCategoryNameExactlyOnce()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetTrips += null, null, new CategoriesEventArgs());

            tripGetServiceMock.Verify(x => x.GetTripsByCategoryName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void TripGetService_WhenGetTripsIsCalled_ShouldCalleGetTripsByCategoryNameWithCorrectParams()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);
            var categoryName = "_";
            var skip = 0;
            var take = 0;

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetTrips += null, null, new CategoriesEventArgs() { CategoryName = categoryName, Skip = skip, Take = take });

            tripGetServiceMock.Verify(x => x.GetTripsByCategoryName(categoryName, skip, take), Times.Once);
        }

        [Test]
        public void ViewModel_WhenGetTripsIsCalled_ShouldSetTripsToTheValueReturnedFromGetTripsByCategoryName()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var trips = new List<ITrip>();
            tripGetServiceMock.Setup(x => x.GetTripsByCategoryName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetTrips += null, null, new CategoriesEventArgs());

            Assert.That(categoryViewMock.Object.Model.Trips.Equals(trips));
        }

        [Test]
        public void CategoryService_WhenGetCategoriesIsRaised_ShouldCallGetAllCategoriesExactlyOnce()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetCategories += null, null, new EventArgs());

            categoryServiceMock.Verify(x => x.GetAllCategories(), Times.Once());
        }


        [Test]
        public void ViewModel_WhenGetCategoriesIsRaised_ShouldSetCategoriesToTheValueReturnedFromGetAllCategories()
        {
            var categoryViewMock = new Mock<ICategoriesView>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var categories = new List<ICategory>();
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(categories);
            var tripGetServiceMock = new Mock<ITripGetService>();
            var modelMock = new Mock<CategoriesModel>();
            categoryViewMock.Setup(x => x.Model).Returns(modelMock.Object);

            var presenter = new CategoriesPresenter(categoryViewMock.Object, categoryServiceMock.Object, tripGetServiceMock.Object);
            categoryViewMock.Raise(x => x.GetCategories += null, null, new EventArgs());

            Assert.That(categoryViewMock.Object.Model.Categories.Equals(categories));
        }
    }
}