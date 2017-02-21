using System;
using NUnit.Framework;
using Moq;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Factories;
using OnTheRoad.Logic.Services;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class SubscriptionServiceTests
    {
        private Mock<ISubscriptionDataUtil> subscriptionDataUtilMock;
        private Mock<ISubscriptionAddHelper> subscriptionAddHelperMock;
        private Mock<ISubscriptionFactory> subscriptionFactoryMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.subscriptionDataUtilMock = new Mock<ISubscriptionDataUtil>();
            this.subscriptionAddHelperMock = new Mock<ISubscriptionAddHelper>();
            this.subscriptionFactoryMock = new Mock<ISubscriptionFactory>();
        }

        [Test]
        public void SubscriptionService_WhenInitializedWithNullForSubscriptionDataUtil_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SubscriptionService(null, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object));
        }

        [Test]
        public void SubscriptionService_WhenInitializedWithNullForSubscriptionDataUtil_ShouldThrowNewArgumentNullExceptionWithProperException()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new SubscriptionService(null, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object));
            StringAssert.Contains("subscriptionDataUtil cannot be null!", exc.Message);
        }

        [Test]
        public void SubscriptionService_WhenInitializedWithNullForSubscriptionAddHelper_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SubscriptionService(subscriptionDataUtilMock.Object, null, subscriptionFactoryMock.Object));
        }

        [Test]
        public void SubscriptionService_WhenInitializedWithNullForSubscriptionAddHelper_ShouldThrowNewArgumentNullExceptionWithProperException()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new SubscriptionService(subscriptionDataUtilMock.Object, null, subscriptionFactoryMock.Object));
            StringAssert.Contains("subscriptionAddHelper cannot be null!", exc.Message);
        }

        [Test]
        public void SubscriptionService_WhenInitializedWithNullForSubscriptionFactory_ShouldThrowNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, null));
        }

        [Test]
        public void SubscriptionService_WhenInitializedWithNullForSubscriptionFactory_ShouldThrowNewArgumentNullExceptionWithProperException()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, null));
            StringAssert.Contains("subscriptionFactory cannot be null!", exc.Message);
        }

        [Test]
        public void SubscriptionDataUtil_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallGetSubscriptionExactlyOnce()
        {
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionDataUtilMock.Verify(x => x.GetSubscription(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void SubscriptionDataUtil_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallGetSubscriptionWithThePassedParams()
        {
            var username = "username";
            var tripId = 0;

            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription(username, tripId, SubscriptionStatus.Attending);

            subscriptionDataUtilMock.Verify(x => x.GetSubscription(username, tripId), Times.Once);
        }

        [Test]
        public void SubscriptionFactory_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallCreateSubscriptionExactlyOnce()
        {
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionFactoryMock.Verify(x => x.CreateSubscription(It.IsAny<SubscriptionStatus>()), Times.Once);
        }

        [Test]
        public void SubscriptionFactory_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallCreateSubscriptionWithThePassedStatus()
        {
            var status = SubscriptionStatus.Attending;
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, status);

            subscriptionFactoryMock.Verify(x => x.CreateSubscription(status), Times.Once);
        }

        [Test]
        public void SubscriptionAddHelper_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallSetSubscriptionUserByUsernameExactlyOnce()
        {
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionAddHelperMock.Verify(x => x.SetSubscriptionUserByUsername(It.IsAny<ISubscription>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void SubscriptionAddHelper_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallSetSubscriptionUserByUsernameWithTheCorrectParams()
        {
            var username = "username";
            var subscriptionMock = new Mock<ISubscription>();
            this.subscriptionFactoryMock.Setup(x => x.CreateSubscription(It.IsAny<SubscriptionStatus>())).Returns(subscriptionMock.Object);
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription(username, 0, SubscriptionStatus.Attending);

            subscriptionAddHelperMock.Verify(x => x.SetSubscriptionUserByUsername(subscriptionMock.Object, username), Times.Once);
        }

        [Test]
        public void SubscriptionAddHelper_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallSetSubscriptionTripByIdExactlyOnce()
        {
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionAddHelperMock.Verify(x => x.SetSubscriptionTripById(It.IsAny<ISubscription>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void SubscriptionAddHelper_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallSetSubscriptionTripByIdWithTheCorrectParams()
        {
            var tripId = 0;
            var subscriptionMock = new Mock<ISubscription>();
            this.subscriptionFactoryMock.Setup(x => x.CreateSubscription(It.IsAny<SubscriptionStatus>())).Returns(subscriptionMock.Object);
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", tripId, SubscriptionStatus.Attending);

            subscriptionAddHelperMock.Verify(x => x.SetSubscriptionTripById(subscriptionMock.Object, tripId), Times.Once);
        }

        [Test]
        public void SubscriptionDataUtil_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallAddSubscriptionExactlyOnce()
        {
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionDataUtilMock.Verify(x => x.AddSubscription(It.IsAny<ISubscription>()), Times.Once);
        }

        [Test]
        public void SubscriptionDataUtil_WhenAddOrUpdateSubscriptionIsCalled_ShouldCallAddSubscriptionWithTheCorrectParams()
        {
            var subscriptionMock = new Mock<ISubscription>();
            this.subscriptionFactoryMock.Setup(x => x.CreateSubscription(It.IsAny<SubscriptionStatus>())).Returns(subscriptionMock.Object);
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionDataUtilMock.Verify(x => x.AddSubscription(subscriptionMock.Object), Times.Once);
        }

        [Test]
        public void SubscriptionDataUtil_WhenAddOrUpdateSubscriptionIsCalledAndThereIsSuchSubscriptions_ShouldCallUpdateSubscriptionExactlyOnce()
        {
            var subscriptionMock = new Mock<ISubscription>();
            this.subscriptionDataUtilMock.Setup(x => x.GetSubscription(It.IsAny<string>(), It.IsAny<int>())).Returns(subscriptionMock.Object);
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionDataUtilMock.Verify(x => x.UpdateSubscription(It.IsAny<ISubscription>()), Times.Once);
        }

        [Test]
        public void SubscriptionDataUtil_WhenAddOrUpdateSubscriptionIsCalledAndThereIsSuchSubscriptions_ShouldCallUpdateSubscriptionWithTheCorrectParameter()
        {
            var subscriptionMock = new Mock<ISubscription>();
            this.subscriptionDataUtilMock.Setup(x => x.GetSubscription(It.IsAny<string>(), It.IsAny<int>())).Returns(subscriptionMock.Object);
            var service = new SubscriptionService(subscriptionDataUtilMock.Object, subscriptionAddHelperMock.Object, subscriptionFactoryMock.Object);
            service.AddOrUpdateSubscription("username", 0, SubscriptionStatus.Attending);

            subscriptionDataUtilMock.Verify(x => x.UpdateSubscription(subscriptionMock.Object), Times.Once);
        }
    }
}