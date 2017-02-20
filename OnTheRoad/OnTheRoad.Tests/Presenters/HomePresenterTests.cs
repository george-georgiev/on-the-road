using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Mvp.Views;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.Presenters
{
    [TestFixture]
    public class HomePresenterTests
    {
        [Test]
        public void WhenHomePresenterIsInitialized_WithNullForTripsService_ArgumentNullException_ShouldBeThrown()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedUserService = new Mock<IUserGetService>();

            Assert.Throws<ArgumentNullException>(() => new HomePresenter(mockedHomeView.Object, null, mockedUserService.Object));
        }

        [Test]
        public void WhenHomePresenterIsInitialized_WithNullForTripsService_ShouldReturnProperErrorMessage()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedUserService = new Mock<IUserGetService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new HomePresenter(mockedHomeView.Object, null, mockedUserService.Object));
            StringAssert.Contains("tripsService cannot be null!", exc.Message);
        }

        [Test]
        public void WhenHomePresenterIsInitialized_WithNullForUsersService_ArgumentNullException_ShouldBeThrown()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedTripsService = new Mock<ITripGetService>();

            Assert.Throws<ArgumentNullException>(() => new HomePresenter(mockedHomeView.Object, mockedTripsService.Object, null));
        }

        [Test]
        public void WhenHomePresenterIsInitialized_WithNullForUsersService_ShouldReturnProperErrorMessage()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedTripsService = new Mock<ITripGetService>();

            var exc = Assert.Throws<ArgumentNullException>(() => new HomePresenter(mockedHomeView.Object, mockedTripsService.Object, null));
            StringAssert.Contains("usersService cannot be null!", exc.Message);
        }

        [Test]
        public void UserService_WhenGetAllUsersCountIsRaise_ShouldCallGetAllUsersCountExactlyOnce()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedTripsService = new Mock<ITripGetService>();
            var mockedUserService = new Mock<IUserGetService>();

            var homeModelMock = new Mock<HomeModel>();
            mockedHomeView.Setup(x => x.Model).Returns(homeModelMock.Object);

            var presenter = new HomePresenter(mockedHomeView.Object, mockedTripsService.Object, mockedUserService.Object);
            mockedHomeView.Raise(x => x.GetAllUsersCount += null, null, new GetHomePageInfoEventArgs() );

            mockedUserService.Verify(x => x.GetAllUsersCount(), Times.Once);
        }

        [Test]
        public void ТripsService_WhenGetAllTripsCountIsRaise_ShouldCallGetTripsCountExactlyOnce()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedTripsService = new Mock<ITripGetService>();
            var mockedUserService = new Mock<IUserGetService>();

            var homeModelMock = new Mock<HomeModel>();
            mockedHomeView.Setup(x => x.Model).Returns(homeModelMock.Object);

            var presenter = new HomePresenter(mockedHomeView.Object, mockedTripsService.Object, mockedUserService.Object);
            mockedHomeView.Raise(x => x.GetAllTripsCount += null, null, new GetHomePageInfoEventArgs());

            mockedTripsService.Verify(x => x.GetTripsCount(), Times.Once);
        }

        [Test]
        public void ТripsService_WhenGetRecentTripsIsRaise_ShouldCallGetTripsExactlyOnce()
        {
            var mockedHomeView = new Mock<IHomeView>();
            var mockedTripsService = new Mock<ITripGetService>();
            var mockedUserService = new Mock<IUserGetService>();

            var homeModelMock = new Mock<HomeModel>();
            mockedHomeView.Setup(x => x.Model).Returns(homeModelMock.Object);

            var presenter = new HomePresenter(mockedHomeView.Object, mockedTripsService.Object, mockedUserService.Object);
            mockedHomeView.Raise(x => x.GetRecentTrips += null, null, new GetHomePageInfoEventArgs());

            mockedTripsService.Verify(x => x.GetTrips(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
