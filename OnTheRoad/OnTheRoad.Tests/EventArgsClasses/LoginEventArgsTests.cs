using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class LoginEventArgsTests
    {
        [Test]
        public void WhenLoginEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new LoginEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<LoginEventArgs>());
        }

        [Test]
        public void VerifyThatUsernameCanBeGettedSetted()
        {
            var loginEventArgs = new LoginEventArgs();
            var username = "SomeRandomEmail";

            loginEventArgs.Username = username;

            Assert.That(loginEventArgs.Username.Equals(username));
        }

        [Test]
        public void VerifyThatUserPasswordCanBeGettedSetted()
        {
            var loginEventArgs = new LoginEventArgs();
            var userPassword = "SomeRandompassword";

            loginEventArgs.UserPassword = userPassword;

            Assert.That(loginEventArgs.UserPassword.Equals(userPassword));
        }

        [Test]
        public void VerifyThatRememberMePropertyCanBeGettedSetted()
        {
            var loginEventArgs = new LoginEventArgs();
            var rememberMe = false;

            loginEventArgs.RememberMe = rememberMe;

            Assert.That(loginEventArgs.RememberMe == rememberMe);
        }

        [Test]
        public void VerifyThatOwinContextPropertyCanBeGettedSetted()
        {
            var loginEventArgs = new LoginEventArgs();
            var owinContext = new Mock<IOwinContext>();

            loginEventArgs.OwinContext = owinContext.Object;

            Assert.That(loginEventArgs.OwinContext.Equals(owinContext.Object));
        }
    }
}
