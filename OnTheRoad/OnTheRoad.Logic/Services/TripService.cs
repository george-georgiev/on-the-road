using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Services
{
    public class TripService : ITripGetService, ITripAddService
    {
        private readonly ITripDataUtil tripDataUtil;
        private readonly ITripAddHelper tripAddHelper;

        public TripService(ITripDataUtil tripDataUtil, ITripAddHelper tripAddHelper)
        {
            if (tripDataUtil == null)
            {
                throw new ArgumentNullException("tripDataUtil can not be null!");
            }

            if (tripAddHelper == null)
            {
                throw new ArgumentNullException("tripAddHelper can not be null!");
            }

            this.tripDataUtil = tripDataUtil;
            this.tripAddHelper = tripAddHelper;
        }

        public void AddTrip(ITrip trip, string organiserUsername, IEnumerable<int> categoryIds, IEnumerable<string> tagNames)
        {
            this.tripAddHelper.SetTripCategoriesById(trip, categoryIds);

            this.tripAddHelper.SetTripTagsByName(trip, tagNames);

            this.tripAddHelper.SetTripOrganiserByUsername(trip, organiserUsername);

            trip.CreateDate = DateTime.Now;
            this.tripDataUtil.AddTrip(trip);
        }

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take)
        {
            var trips = this.tripDataUtil.GetTripsByCategoryName(categoryName, skip, take);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending = false)
        {
            var trips = this.tripDataUtil.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending);

            return trips;
        }

        public int GetTripsCountByCategoryName(string categoryName)
        {
            var count = this.tripDataUtil.GetTripsCountByCategoryName(categoryName);

            return count;
        }

        public ITrip GetTripById(int tripId)
        {
            var trip = this.tripDataUtil.GetTripById(tripId);

            return trip;
        }

        public IEnumerable<ITrip> GetTripsBySearchPattern(string pattern, int skip, int take)
        {
            var trips = this.tripDataUtil.GetTripsBySearchPattern(pattern, skip, take);

            return trips;
        }

        public int GetTripsCountBySearchPattern(string pattern)
        {
            var count = this.tripDataUtil.GetTripsCountBySearchPattern(pattern);

            return count;
        }

        public IEnumerable<ITrip> GetTrips(int skip, int take)
        {
            var trips = this.tripDataUtil.GetTrips(skip, take);

            return trips;
        }

        public int GetTripsCount()
        {
            var count = this.tripDataUtil.GetTripsCount();

            return count;
        }
    }
}
