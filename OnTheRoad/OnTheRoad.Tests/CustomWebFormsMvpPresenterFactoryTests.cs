using System;
using Moq;
using NUnit.Framework;
using OnTheRoad.Account.Contracts;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.Presenters;
using WebFormsMvp;

namespace OnTheRoad.Tests
{
    [TestFixture]
    public class CustomWebFormsMvpPresenterFactoryTests
    {
        [Test]
        public void NewInstance_ShouldThrowArgumentNullException_WhenICustomPresenterFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomWebFormsMvpPresenterFactory(null));
        }

        [Test]
        public void NewInstance_ShouldThrowArgumentNullException_WithProperExceptionMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new CustomWebFormsMvpPresenterFactory(null));
            StringAssert.Contains("Value cannot be null", exc.Message);
        }

        [Test]
        public void WhenCustomWebFormsMvpPresenterFactoryIsInitialized_InstanceShouldBeCreated()
        {
            var customPresenterFactoryMock = new Mock<ICustomPresenterFactory>();

            var actualInstance = new CustomWebFormsMvpPresenterFactory(customPresenterFactoryMock.Object);

            Assert.That(actualInstance, Is.Not.Null);
        }

        [Test]
        public void WhenCustomWebFormsMvpPresenterFactoryIsInitialized_InstanceOfTheClassShouldBeCreated()
        {
            var customPresenterFactoryMock = new Mock<ICustomPresenterFactory>();

            var actualInstance = new CustomWebFormsMvpPresenterFactory(customPresenterFactoryMock.Object);

            Assert.That(actualInstance, Is.InstanceOf<CustomWebFormsMvpPresenterFactory>());
        }

        [Test]
        public void WhenCreateIsCalledWithNullParameterForPresenterType_ArgumentNullExcMustBeThrown()
        {
            var customPresenterFactoryMock = new Mock<ICustomPresenterFactory>();
            var viewMock = new Mock<IView>();

            var factory = new CustomWebFormsMvpPresenterFactory(customPresenterFactoryMock.Object);

            Assert.Throws<ArgumentNullException>(() => factory.Create(null, null, viewMock.Object));
        }

        [Test]
        public void WhenCreateIsCalledWithNullParameterForPresenterType_ArgumentNullExcMustBeThrownWithProperExceptionMessage()
        {
            var customPresenterFactoryMock = new Mock<ICustomPresenterFactory>();
            var viewMock = new Mock<IView>();

            var factory = new CustomWebFormsMvpPresenterFactory(customPresenterFactoryMock.Object);

            var exc = Assert.Throws<ArgumentNullException>(() => factory.Create(null, null, viewMock.Object));
            StringAssert.Contains("Value cannot be null", exc.Message);
        }

        [Test]
        public void VerifyThatPresenterFactoryCallsCreateMethodIsExactlyOnce()
        {
            var customPresenterFactoryMock = new Mock<ICustomPresenterFactory>();
            var viewMock = new Mock<ILoginView>();
            var presenterMock = new Mock<LoginPresenter>();

            var factory = new CustomWebFormsMvpPresenterFactory(customPresenterFactoryMock.Object);
            var presenter = factory.Create(presenterMock.GetType(), viewMock.GetType(), viewMock.Object);

            customPresenterFactoryMock.Verify(x => x.GetPresenter(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<IView>()), Times.Once);
        }

        [Test]
        public void VerifyThatPresenterFactory_ReturnsInstanceOfIPresenter_WhenCreateMethodIsCalled()
        {
            var customPresenterFactoryMock = new Mock<ICustomPresenterFactory>();
            var viewMock = new Mock<ILoginView>();
            var presenterMock = new Mock<LoginPresenter>();
            var iPresenterMock = new Mock<IPresenter>();
            customPresenterFactoryMock.Setup(x => x.GetPresenter(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<IView>())).Returns(iPresenterMock.Object);

            var factory = new CustomWebFormsMvpPresenterFactory(customPresenterFactoryMock.Object);
            var actualInstance = factory.Create(presenterMock.GetType(), viewMock.GetType(), viewMock.Object);

            Assert.That(actualInstance, Is.InstanceOf<IPresenter>());
        }
    }
}
