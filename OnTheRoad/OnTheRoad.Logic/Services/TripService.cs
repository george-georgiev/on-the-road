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

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName)
        {
            var trips = this.tripDataUtil.GetTripsByCategoryName(categoryName);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending = false)
        {
            var trips = this.tripDataUtil.GetTripsOrderedByDateCreated(count, isAscending);

            return trips;
        }
    }
}
