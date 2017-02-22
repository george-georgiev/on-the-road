using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Services;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.categoryRepositoryMock = new Mock<ICategoryRepository>();
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void CategoryService_WhenInitializedWithNullForcategoryRepository_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CategoryService( null, unitOfWorkMock.Object));
        }

        [Test]
        public void CategoryService_WhenInitializedWithNullForcategoryRepository_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new CategoryService(null, unitOfWorkMock.Object));
            StringAssert.Contains("categoryRepository cannot be null!", exc.Message);
        }

        [Test]
        public void CategoryService_WhenInitializedWithNullForUnitOfWork_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CategoryService(categoryRepositoryMock.Object, null));
        }

        [Test]
        public void CategoryService_WhenInitializedWithNullForUnitOfWork_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new CategoryService(categoryRepositoryMock.Object, null));
            StringAssert.Contains("uniOfWork cannot be null!", exc.Message);
        }

        [Test]
        public void CategoryRepository_WhenGetAllCategoriesIsCalled_ShouldCallGetAllExactlyOnce()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);

            service.GetAllCategories();

            this.categoryRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetCategoriesByIdCollection_WhenCalled_ShouldReturnInstanceOfIEnumerableFromICategory()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = service.GetCategoriesByIdCollection(new List<int>());

            Assert.That(actual, Is.InstanceOf<IEnumerable<ICategory>>());
        }

        [Test]
        public void GetCategoriesByIdCollection_WhenCalledWithEmptyCollection_ShouldReturnEmptyCollection()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = service.GetCategoriesByIdCollection(new List<int>());

            Assert.That(actual, Is.Empty);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(44)]
        [TestCase(12)]
        [Test]
        public void CategoryRepository_WhenGetCategoriesByIdCollectionIsCalled_ShouldCallGetByIdExactNumberOfTimes(int count)
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var idCollection = new List<int>();
            for (int i = 0; i < count; i++)
            {
                idCollection.Add(i);
            }

            var actual = service.GetCategoriesByIdCollection(idCollection);

            categoryRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Exactly(count));
        }

        [Test]
        public void CategoryRepository_WhenGetCategoryByIdIsCalled_ShouldCallGetByIdExactOnce()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = service.GetCategoryById(12);

            categoryRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CategoryRepository_WhenGetCategoryByIdIsCalledWithUnexistingCategory_ShouldReturnNull()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = service.GetCategoryById(12);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void CategoryRepository_WhenGetCategoryByIdIsCalledWithExistingCategory_InstanceOfICategory()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            categoryRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Mock<ICategory>().Object);
            var actual = service.GetCategoryById(12);

            Assert.That(actual, Is.InstanceOf<ICategory>());
        }

        [Test]
        public void CategoryRepository_WhenGetCategoryByNameIsCalledWithExistingCategory_InstanceOfICategory()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            categoryRepositoryMock.Setup(x => x.GetCategoryByName(It.IsAny<string>())).Returns(new Mock<ICategory>().Object);
            var actual = service.GetCategoryByName("_");

            Assert.That(actual, Is.InstanceOf<ICategory>());
        }

        [Test]
        public void CategoryRepository_WhenGetCategoryByNameIsCalledWithUnexistingCategory_ShouldReturnNull()
        {
            var service = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var actual = service.GetCategoryByName("_");

            Assert.That(actual, Is.Null);
        }
    }
}
