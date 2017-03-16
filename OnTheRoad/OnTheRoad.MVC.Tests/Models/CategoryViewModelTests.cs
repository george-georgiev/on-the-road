using NUnit.Framework;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.MVC.Tests.Models
{
    public class CategoryViewModelTests
    {
        [Test]
        public void Constructor_ShouldReturnCategoryDetailsViewModelInstance()
        {
            // Arrange & Act & Assert
            Assert.IsInstanceOf<CategoryViewModel>(new CategoryViewModel());
        }

        [Test]
        public void Name_ShouldGetSetCorrectValue()
        {
            // Arrange
            var categoryModel = new CategoryViewModel();
            var name = "SomeName";

            // Act
            categoryModel.Name = name;

            // Assert
            Assert.AreEqual(name, categoryModel.Name);
        }
    }
}
