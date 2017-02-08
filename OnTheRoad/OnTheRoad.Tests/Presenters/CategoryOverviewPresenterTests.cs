using Moq;
using NUnit.Framework;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class CategoryOverviewPresenterTests
    {
        [Test]
        public void WhenCategoryOverviewPresenterIsInitialized_WithNull_ArgumentNullException_ShouldBeThrown()
        {
            var mockedCategoryOverviewView = new Mock<ICategoryOverviewView>();

            Assert.Throws<ArgumentNullException>(() => new CategoryOverviewPresenter(mockedCategoryOverviewView.Object, null));
        }
    }
}
