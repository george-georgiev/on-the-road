using NUnit.Framework;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class ProfileImageEventArgsTests
    {
        [Test]
        public void WhenProfileImageEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new ProfileImageEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<ProfileImageEventArgs>());
        }

        [Test]
        public void VerifyThatUsernameCanBeGettedSetted()
        {
            var profileImageEventArgs = new ProfileImageEventArgs();
            var user = "user";

            profileImageEventArgs.UserName = user;

            Assert.That(profileImageEventArgs.UserName.Equals(user));
        }

        [Test]
        public void VerifyThatUsernameReturnsInstanceOfString()
        {
            var profileImageEventArgs = new ProfileImageEventArgs();
            var user = "user";

            profileImageEventArgs.UserName = user;

            Assert.That(profileImageEventArgs.UserName, Is.InstanceOf<string>());
        }

        [Test]
        public void VerifyThatImageCanBeGettedSetted()
        {
            var profileImageEventArgs = new ProfileImageEventArgs();
            var image = new byte[1];

            profileImageEventArgs.Image = image;

            Assert.That(profileImageEventArgs.Image.Equals(image));
        }

        [Test]
        public void VerifyThatImageReturnsInstanceOfString()
        {
            var profileImageEventArgs = new ProfileImageEventArgs();
            var image = new byte[1];

            profileImageEventArgs.Image = image;

            Assert.That(profileImageEventArgs.Image, Is.InstanceOf<byte[]>());
        }
    }
}