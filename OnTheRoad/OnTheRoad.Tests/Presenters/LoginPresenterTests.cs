using System;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using OnTheRoad.Account.Contracts;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.Enums;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Models;
using OnTheRoad.Presenters;

namespace OnTheRoad.Presenters.Tests
{
    [TestFixture]
    public class LoginPresenterTest
    {
        [Test]
        public void WhenLoginPresenterIsInitialized_WithNull_ArgumentNullException_ShouldBeThrown()
        {
            var mockedLoginView = new Mock<ILoginView>();

            Assert.Throws<ArgumentNullException>(() => new LoginPresenter(mockedLoginView.Object, null));
        }

        [Test]
        public void WhenLoginPresenterIsInitialized_WithNull_ShouldReturnProperErrorMessage()
        {
            var mockedLoginView = new Mock<ILoginView>();

            var exc = Assert.Throws<ArgumentNullException>(() => new LoginPresenter(mockedLoginView.Object, null));
            StringAssert.Contains("Authentication Factory cannot be null", exc.Message);
        }

        [Test]
        public void WhenLoginPresenterIsInitialized_InstanceShouldBeCreated()
        {
            var mockedLoginView = new Mock<ILoginView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();

            var actualInstance = new LoginPresenter(mockedLoginView.Object, mockedAuthServiceFactory.Object);
            Assert.That(actualInstance, Is.Not.Null);
        }

        [Test]
        public void WhenLoginPresenterIsInitialized_InstanceOfLoginPresenterShouldBeReturned()
        {
            var mockedLoginView = new Mock<ILoginView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();

            var loginPresenter = new LoginPresenter(mockedLoginView.Object, mockedAuthServiceFactory.Object);
            Assert.That(loginPresenter, Is.InstanceOf<LoginPresenter>());
        }

        [Test]
        public void WhenLoginPresenterIsInitialized_AuthServiceFactory_ShouldCall_GetLoginServiceExactlyOnce()
        {
            var mockedLoginView = new Mock<ILoginView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedloginPresenter = new Mock<LoginPresenter>();
            var mockedLoginSerive = new Mock<ILoginService>();
            var mockedModel = new Mock<LoginModel>();

            mockedLoginView.Setup(x => x.Model).Returns(mockedModel.Object);
            mockedLoginSerive.Setup(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns("Success");
            mockedAuthServiceFactory.Setup(x => x.GetLoginService(It.IsAny<IOwinContext>())).Returns(mockedLoginSerive.Object);

            var loginPresenter = new LoginPresenter(mockedLoginView.Object, mockedAuthServiceFactory.Object);
            mockedLoginView.Raise(x => x.LoginUser += null, null, new LoginEventArgs());

            mockedAuthServiceFactory.Verify(x => x.GetLoginService(It.IsAny<IOwinContext>()), Times.Once);
        }

        [Test]
        public void WhenLoginPresenterIsInitialized_LoginService_ShouldCall_LoginUserExactlyOnce()
        {
            var mockedLoginView = new Mock<ILoginView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedloginPresenter = new Mock<LoginPresenter>();
            var mockedLoginSerive = new Mock<ILoginService>();
            var mockedModel = new Mock<LoginModel>();

            mockedLoginView.Setup(x => x.Model).Returns(mockedModel.Object);
            mockedLoginSerive.Setup(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns("Success");
            mockedAuthServiceFactory.Setup(x => x.GetLoginService(It.IsAny<IOwinContext>())).Returns(mockedLoginSerive.Object);

            var loginPresenter = new LoginPresenter(mockedLoginView.Object, mockedAuthServiceFactory.Object);
            mockedLoginView.Raise(x => x.LoginUser += null, null, new LoginEventArgs());

            mockedLoginSerive.Verify(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [TestCase("Success")]
        [TestCase("LockedOut")]
        [TestCase("Failure")]
        [Test]
        public void WhenLoginPresenterIsInitialized_ViewModelLoginStatus_ShouldReturnTheCorrectLoginStatus(string loginStatus)
        {
            var mockedLoginView = new Mock<ILoginView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedloginPresenter = new Mock<LoginPresenter>();
            var mockedLoginSerive = new Mock<ILoginService>();
            var mockedModel = new Mock<LoginModel>();

            mockedLoginView.Setup(x => x.Model).Returns(mockedModel.Object);
            mockedLoginSerive.Setup(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(loginStatus);
            mockedAuthServiceFactory.Setup(x => x.GetLoginService(It.IsAny<IOwinContext>())).Returns(mockedLoginSerive.Object);

            var loginPresenter = new LoginPresenter(mockedLoginView.Object, mockedAuthServiceFactory.Object);
            mockedLoginView.Raise(x => x.LoginUser += null, null, new LoginEventArgs());

            Assert.That(mockedLoginView.Object.Model.LoginStatus == (LoginStatus)Enum.Parse(typeof(LoginStatus), loginStatus, true));
        }
    }
}
