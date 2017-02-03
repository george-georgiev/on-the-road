using Microsoft.Owin;
using System;
using Moq;
using NUnit.Framework;
using OnTheRoad.Account.Interfaces;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Presenters.Tests.Account.Fakes;

namespace OnTheRoad.Presenters.Account.Tests
{
    [TestFixture]
    public class RegisterPresterTest
    {
        [Test]
        public void WhenRegisterPresenterIsInitialized_WithNull_ArgumentNullException_ShouldBeThrown()
        {
            var mockedRegView = new Mock<IRegisterView>();

            Assert.Throws<ArgumentNullException>(() => new RegisterPresenter(mockedRegView.Object, null));
        }

        [Test]
        public void WhenRegisterPresenterIsInitialized_WithNull_ShouldReturnProperErrorMessage()
        {
            var mockedRegView = new Mock<IRegisterView>();

            var exc = Assert.Throws<ArgumentNullException>(() => new RegisterPresenter(mockedRegView.Object, null));
            StringAssert.Contains("Authentication Factory cannot be null", exc.Message);
        }

        [Test]
        public void WhenRegisterPresenterIsInitialized_InstanceShouldBeCreated()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();

            var actualInstance = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            Assert.That(actualInstance, Is.Not.Null);
        }

        [Test]
        public void WhenRegisterPresenterIsInitialized_InstanceOfRegisterPresenterShouldBeReturned()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();

            var registerPresenter = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            Assert.That(registerPresenter, Is.InstanceOf<RegisterPresenter>());
        }

        [Test]
        public void WhenRegisterPresenterIsInitialized_View_ShouldSubscribeTo_CreateUserEvent()
        {
            var fakeRegView = new FakeRegisterView();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();

            var registerPresenter = new RegisterPresenter(fakeRegView, mockedAuthServiceFactory.Object);
            Assert.That(fakeRegView.subscribedMethod.Equals("Create_User"));
        }

        [Test]
        public void WhenViewSubscribesToCreateUser_ParameterClassName_ShouldBe_RegisterEventArgs()
        {
            var fakeRegView = new FakeRegisterView();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();

            var registerPresenter = new RegisterPresenter(fakeRegView, mockedAuthServiceFactory.Object);
            Assert.That(fakeRegView.parameterClassName.Equals("RegisterEventArgs"));
        }

        [Test]
        public void WhenViewInvokesCreateUser_GetRegisterService_ShouldBeCalledExactlyOnce()
        {
            var fakeRegView = new FakeRegisterView();
            var mockedRegEventArgs = new Mock<RegisterEventArgs>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);

            var registerPresenter = new RegisterPresenter(fakeRegView, mockedAuthServiceFactory.Object);
            fakeRegView.InvokeGetCreateUser(mockedRegEventArgs.Object);

            mockedAuthServiceFactory.Verify(x => x.GetRegisterService(It.IsAny<IOwinContext>()), Times.Once());
        }

        [Test]
        public void WhenViewInvokesCreateUser_ModelHasSucceeded_ShouldBeTrue()
        {
            var fakeRegView = new FakeRegisterView();
            var mockedRegEventArgs = new Mock<RegisterEventArgs>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);

            var registerPresenter = new RegisterPresenter(fakeRegView, mockedAuthServiceFactory.Object);
            fakeRegView.InvokeGetCreateUser(mockedRegEventArgs.Object);

            Assert.That(fakeRegView.Model.HasSucceeded, Is.True);
        }

        [Test]
        public void WhenViewInvokesCreateUser_ModelHasSucceeded_ShouldBeFalse_WhenArgumentExceptionIsThrown()
        {
            var fakeRegView = new FakeRegisterView();
            var mockedRegEventArgs = new Mock<RegisterEventArgs>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegService.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException());

            var registerPresenter = new RegisterPresenter(fakeRegView, mockedAuthServiceFactory.Object);
            fakeRegView.InvokeGetCreateUser(mockedRegEventArgs.Object);

            Assert.That(fakeRegView.Model.HasSucceeded, Is.False);
        }

        [Test]
        public void WhenViewInvokesCreateUser_ModelHasSucceeded_ShouldReturnProperError_WhenArgumentExceptionIsThrown()
        {
            var fakeRegView = new FakeRegisterView();
            var mockedRegEventArgs = new Mock<RegisterEventArgs>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegService.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException());

            var registerPresenter = new RegisterPresenter(fakeRegView, mockedAuthServiceFactory.Object);
            fakeRegView.InvokeGetCreateUser(mockedRegEventArgs.Object);

            Assert.That(fakeRegView.Model.ErrorMsg.Equals("Value does not fall within the expected range."));
        }
    }
}
