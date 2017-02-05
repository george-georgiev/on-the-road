using System;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using OnTheRoad.Account.Contracts;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Models;

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
        public void WhenViewInvokesCreateUser_GetRegisterService_ShouldBeCalledExactlyOnce()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();
            var mockedModel = new Mock<RegisterModel>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegView.Setup(x => x.Model).Returns(mockedModel.Object);

            var registerPresenter = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            mockedRegView.Raise(x => x.CreateUser += null, null, new RegisterEventArgs());
            
            mockedAuthServiceFactory.Verify(x => x.GetRegisterService(It.IsAny<IOwinContext>()), Times.Once());
        }

        [Test]
        public void WhenViewInvokesCreateUser_ModelHasSucceeded_ShouldBeTrue()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();
            var mockedModel = new Mock<RegisterModel>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegView.Setup(x => x.Model).Returns(mockedModel.Object);

            var registerPresenter = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            mockedRegView.Raise(x => x.CreateUser += null, null, new RegisterEventArgs());

            Assert.That(mockedRegView.Object.Model.HasSucceeded, Is.True);
        }

        [Test]
        public void WhenViewInvokesCreateUser_RegisterService_ShouldCall_CreateUserExactlyOnce()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();
            var mockedModel = new Mock<RegisterModel>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegView.Setup(x => x.Model).Returns(mockedModel.Object);

            var registerPresenter = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            mockedRegView.Raise(x => x.CreateUser += null, null, new RegisterEventArgs());

            mockedRegService.Verify(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void WhenViewInvokesCreateUser_ModelHasSucceeded_ShouldBeFalse_WhenArgumentExceptionIsThrown()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();
            var mockedModel = new Mock<RegisterModel>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegView.Setup(x => x.Model).Returns(mockedModel.Object);
            mockedRegService.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException());

            var registerPresenter = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            mockedRegView.Raise(x => x.CreateUser += null, null, new RegisterEventArgs());

            Assert.That(mockedRegView.Object.Model.HasSucceeded, Is.False);
        }

        [Test]
        public void WhenViewInvokesCreateUser_ModelHasSucceeded_ShouldReturnProperError_WhenArgumentExceptionIsThrown()
        {
            var mockedRegView = new Mock<IRegisterView>();
            var mockedAuthServiceFactory = new Mock<IAuthenticationServiceFactory>();
            var mockedRegService = new Mock<IRegisterService>();
            var mockedModel = new Mock<RegisterModel>();

            mockedAuthServiceFactory.Setup(x => x.GetRegisterService(It.IsAny<IOwinContext>())).Returns(mockedRegService.Object);
            mockedRegView.Setup(x => x.Model).Returns(mockedModel.Object);
            mockedRegService.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException());

            var registerPresenter = new RegisterPresenter(mockedRegView.Object, mockedAuthServiceFactory.Object);
            mockedRegView.Raise(x => x.CreateUser += null, null, new RegisterEventArgs());

            Assert.That(mockedRegView.Object.Model.ErrorMsg.Equals("Value does not fall within the expected range."));
        }
    }
}
