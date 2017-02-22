using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Services;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private Mock<IUnitOfWork> uniOfWorkMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.categoryRepositoryMock = new Mock<ICategoryRepository>();
            this.uniOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void CategoryService_WhenInitializedWithNullForcategoryRepository_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CategoryService( null, uniOfWorkMock.Object));
        }

        [Test]
        public void CategoryService_WhenInitializedWithNullForcategoryRepository_ShouldThrowNewArgumentNullExceptionWithProperMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new CategoryService(null, uniOfWorkMock.Object));
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
    }
}
