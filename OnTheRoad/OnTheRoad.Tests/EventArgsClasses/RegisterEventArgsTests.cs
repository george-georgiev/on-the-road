using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class RegisterEventArgsTests
    {
        [Test]
        public void WhenRegisterEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new RegisterEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<RegisterEventArgs>());
        }

        [Test]
        public void VerifyThatUserEmailCanBeGettedSetted()
        {
            var registerEventArgs = new RegisterEventArgs();
            var email = "SomeRandomEmail";

            registerEventArgs.UserEmail = email;

            Assert.That(registerEventArgs.UserEmail.Equals(email));
        }

        [Test]
        public void VerifyThatUserPasswordCanBeGettedSetted()
        {
            var registerEventArgs = new RegisterEventArgs();
            var userPassword = "SomeRandompassword";

            registerEventArgs.UserPassword = userPassword;

            Assert.That(registerEventArgs.UserPassword.Equals(userPassword));
        }

        [Test]
        public void VerifyThatOwinContextPropertyCanBeGettedSetted()
        {
            var registerEventArgs = new RegisterEventArgs();
            var owinContext = new Mock<IOwinContext>();

            registerEventArgs.OwinContext = owinContext.Object;

            Assert.That(registerEventArgs.OwinContext.Equals(owinContext.Object));
        }
    }
}
