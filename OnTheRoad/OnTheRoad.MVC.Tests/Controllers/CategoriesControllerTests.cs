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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Tests.Controllers
{
    [TestFixture]
    public class CategoriesControllerTests
    {
        [Test]
        public void Constructor_WhenCategoryServiceIsNull_ShouldThrow()
        {
            // Arrange
            var tripGetServiceMock = new Mock<ITripGetService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoriesController(null, tripGetServiceMock.Object));
        }
        [Test]
        public void Constructor_WhenTripGetServiceIsNull_ShouldThrow()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoriesController(categoryServiceMock.Object, null));
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnViewResultInstance()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnDefaultView()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Index() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void Index_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var attributes = categoriesController.GetType().GetMethod("Index").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Index_WhenCalled_ShouldCallGetAllCategoriesOnce()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            categoriesController.Index();

            // Assert
            categoryServiceMock.Verify(x => x.GetAllCategories(), Times.Once);
        }

        [Test]
        public void Index_WhenCalled_ShouldPassCorrectCollectionOfViewModels()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var categoryMock = new Mock<ICategory>();
            var categories = new List<ICategory>() { categoryMock.Object };
            categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(categories);

            var autoMapperMock = new Mock<IMapper>();
            var categoryModelMock = new Mock<CategoryViewModel>();
            autoMapperMock.Setup(x => x.Map<CategoryViewModel>(It.Is<ICategory>(o => o.Equals(categoryMock.Object)))).Returns(categoryModelMock.Object);
            MapperProvider.Mapper = autoMapperMock.Object;

            var tripGetServiceMock = new Mock<ITripGetService>();

            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Index() as ViewResult;
            var model = result.Model as List<CategoryViewModel>;

            // Assert
            Assert.AreSame(categoryModelMock.Object, model[0]);
        }

        [Test]
        public void Details_WhenCalled_ShouldReturnViewResultInstance()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var categoryName = "CategoryName";
            var page = 1;

            // Act
            var result = categoriesController.Details(categoryName, page);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Details_WhenCalled_ShouldReturnDefaultView()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var categoryName = "CategoryName";
            var page = 1;

            // Act
            var result = categoriesController.Details(categoryName, page) as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void Details_ShouldBeDecoratedWithHttpGetAttribute()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var attributes = categoriesController.GetType().GetMethod("Details").GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Act & Assert
            Assert.IsTrue(attributes.Any());
        }

        [Test]
        public void Details_WhenCategoryNameIsNull_ShouldCallGetTripsOnceWithCorrectParameters()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var page = 5;
            var take = 3;
            var skip = (page - 1) * take;

            // Act
            categoriesController.Details(null, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTrips(It.Is<int>(o => o == skip), It.Is<int>(o => o == take)), Times.Once);
        }

        [Test]
        public void Details_WhenCategoryNameIsNotNull_ShouldCallGetTripsByCategoryNameOnceWithCorrectParameters()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var categoryName = "CategoryName";
            var page = 5;
            var take = 3;
            var skip = (page - 1) * take;

            // Act
            categoriesController.Details(categoryName, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTripsByCategoryName(It.Is<string>(o => o == categoryName), It.Is<int>(o => o == skip), It.Is<int>(o => o == take)), Times.Once);
        }

        [Test]
        public void Details_WhenCategoryNameIsNull_ShouldNeverCallGetTripsByCategoryName()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var page = 5;

            // Act
            categoriesController.Details(null, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTripsByCategoryName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Details_WhenCategoryNameIsNotNull_ShouldNeverCallGetTrips()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var categoryName = "CategoryName";
            var page = 5;

            // Act
            categoriesController.Details(categoryName, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Details_WhenCategoryNameIsNull_ShouldCallGetTripsCountOnce()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var page = 5;

            // Act
            categoriesController.Details(null, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTripsCount(), Times.Once);
        }

        [Test]
        public void Details_WhenCategoryNameIsNotNull_ShouldCallGetTripsCountByCategoryNameOnceWithCorrectParameters()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var categoryName = "CategoryName";
            var page = 5;

            // Act
            categoriesController.Details(categoryName, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTripsCountByCategoryName(It.Is<string>(o => o == categoryName)), Times.Once);
        }

        [Test]
        public void Details_WhenCategoryNameIsNull_ShouldNeverCallGetTripsCountByCategoryName()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var page = 5;

            // Act
            categoriesController.Details(null, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTripsCountByCategoryName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Details_WhenCategoryNameIsNotNull_ShouldNeverCallGetTripsCount()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);
            var categoryName = "CategoryName";
            var page = 5;

            // Act
            categoriesController.Details(categoryName, page);

            // Assert
            tripGetServiceMock.Verify(x => x.GetTripsCount(), Times.Never);
        }

        [TestCase(null, 5)]
        [TestCase("", 1)]
        [TestCase("SomeName", -5)]
        [TestCase("OtherName", 0)]
        public void Details_WhenCalled_ShouldSetCorrectTripsPropertyToModel(string categoryName, int page)
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();

            var tripGetServiceMock = new Mock<ITripGetService>();
            var tripMock = new Mock<ITrip>();
            var trips = new List<ITrip>() { tripMock.Object };
            tripGetServiceMock.Setup(x => x.GetTripsByCategoryName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(trips);
            tripGetServiceMock.Setup(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>())).Returns(trips);

            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            var autoMapperMock = new Mock<IMapper>();
            var tripModelMock = new Mock<TripViewModel>();
            autoMapperMock.Setup(x => x.Map<TripViewModel>(It.Is<ITrip>(o => o.Equals(tripMock.Object)))).Returns(tripModelMock.Object);
            MapperProvider.Mapper = autoMapperMock.Object;

            // Act
            var result = categoriesController.Details(categoryName, page) as ViewResult;
            var model = result.Model as CategoryDetailsViewModel;

            // Assert
            Assert.AreSame(tripModelMock.Object, (model.Trips as List<TripViewModel>)[0]);
        }

        [TestCase(null, 5)]
        [TestCase("", 1)]
        [TestCase("SomeName", -5)]
        [TestCase("OtherName", 0)]
        public void Details_WhenCalled_ShouldSetCorrectTotalPropertyToModel(string categoryName, int page)
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();

            var tripGetServiceMock = new Mock<ITripGetService>();
            var total = 5;
            tripGetServiceMock.Setup(x => x.GetTripsCountByCategoryName(It.Is<string>(o => o == categoryName))).Returns(total);
            tripGetServiceMock.Setup(x => x.GetTripsCount()).Returns(total);

            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Details(categoryName, page) as ViewResult;
            var model = result.Model as CategoryDetailsViewModel;

            // Assert
            Assert.AreEqual(total, model.Total);
        }

        [TestCase(null, 5)]
        [TestCase("", 1)]
        [TestCase("SomeName", -5)]
        [TestCase("OtherName", 0)]
        public void Details_WhenCalled_ShouldSetCorrectNamePropertyToModel(string categoryName, int page)
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Details(categoryName, page) as ViewResult;
            var model = result.Model as CategoryDetailsViewModel;

            // Assert
            Assert.AreEqual(categoryName, model.Name);
        }

        [TestCase(null, 5)]
        [TestCase("", 1)]
        [TestCase("SomeName", -5)]
        [TestCase("OtherName", 0)]
        public void Details_WhenCalled_ShouldSetCorrectPagePropertyToModel(string categoryName, int page)
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Details(categoryName, page) as ViewResult;
            var model = result.Model as CategoryDetailsViewModel;

            // Assert
            var expectedPage = page > 0 ? page : 1;
            Assert.AreEqual(expectedPage, model.Page);
        }

        [TestCase(null, 5)]
        [TestCase("", 1)]
        [TestCase("SomeName", -5)]
        [TestCase("OtherName", 0)]
        public void Details_WhenCalled_ShouldSetCorrectTekePropertyToModel(string categoryName, int page)
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryGetService>();
            var tripGetServiceMock = new Mock<ITripGetService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object, tripGetServiceMock.Object);

            // Act
            var result = categoriesController.Details(categoryName, page) as ViewResult;
            var model = result.Model as CategoryDetailsViewModel;

            // Assert
            var take = 3;
            Assert.AreEqual(take, model.Take);
        }
    }
}
