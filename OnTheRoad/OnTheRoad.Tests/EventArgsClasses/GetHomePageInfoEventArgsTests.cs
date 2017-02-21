using System;
using NUnit.Framework;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class GetHomePageInfoEventArgsTests
    {
        [Test]
        public void WhenGetHomePageInfoEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new GetHomePageInfoEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<GetHomePageInfoEventArgs>());
        }

        [Test]
        public void VerifyThatGetRecentTripsCanBeGettedSetted()
        {
            var getHomePageInfoEventArgs = new GetHomePageInfoEventArgs();
            var getRecentTrips = new List<ITrip>();

            getHomePageInfoEventArgs.GetRecentTrips = getRecentTrips;

            Assert.That(getHomePageInfoEventArgs.GetRecentTrips.Equals(getRecentTrips));
        }

        [Test]
        public void VerifyThatGetRecentTripsReturnsInstanceOfIEnumerableFromITrip()
        {
            var getHomePageInfoEventArgs = new GetHomePageInfoEventArgs();
            var getRecentTrips = new List<ITrip>();

            getHomePageInfoEventArgs.GetRecentTrips = getRecentTrips;

            Assert.That(getHomePageInfoEventArgs.GetRecentTrips, Is.InstanceOf<IEnumerable<ITrip>>());
        }

        [Test]
        public void VerifyThatGetUsersCountCanBeGettedSetted()
        {
            var getHomePageInfoEventArgs = new GetHomePageInfoEventArgs();
            var getUsersCount = 12;

            getHomePageInfoEventArgs.GetUsersCount = getUsersCount;

            Assert.That(getHomePageInfoEventArgs.GetUsersCount.Equals(getUsersCount));
        }

        [Test]
        public void VerifyThatGetUsersCountReturnsInstanceOfInt()
        {
            var getHomePageInfoEventArgs = new GetHomePageInfoEventArgs();
            var getUsersCount = 12;

            getHomePageInfoEventArgs.GetUsersCount = getUsersCount;

            Assert.That(getHomePageInfoEventArgs.GetUsersCount, Is.InstanceOf<int>());
        }

        [Test]
        public void VerifyThaGetTripsCountCanBeGettedSetted()
        {
            var getHomePageInfoEventArgs = new GetHomePageInfoEventArgs();
            var getTripsCount = 12;

            getHomePageInfoEventArgs.GetTripsCount = getTripsCount;

            Assert.That(getHomePageInfoEventArgs.GetTripsCount.Equals(getTripsCount));
        }

        [Test]
        public void VerifyThatGetTripsCountReturnsInstanceOfInt()
        {
            var getHomePageInfoEventArgs = new GetHomePageInfoEventArgs();
            var getTripsCount = 12;

            getHomePageInfoEventArgs.GetTripsCount = getTripsCount;

            Assert.That(getHomePageInfoEventArgs.GetTripsCount, Is.InstanceOf<int>());
        }
    }
}
