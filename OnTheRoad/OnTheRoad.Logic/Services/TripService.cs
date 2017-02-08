using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Logic.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            if (tripRepository == null)
            {
                throw new ArgumentNullException("tripRepository can not be null!");
            }

            this.tripRepository = tripRepository;
        }

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName)
        {
            var trips = this.tripRepository.GetTripsByCategoryName(categoryName);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending = false)
        {
            var trips = this.tripRepository.GetTripsOrderedByDateCreated(count, isAscending);

            return trips;
        }
    }
}
