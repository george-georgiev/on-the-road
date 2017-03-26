using OnTheRoad.Logic.Contracts;
using System;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Utils
{
    public class TripDataUtil : ITripDataUtil
    {
        private readonly ITripRepository tripRepository;
        private readonly IUnitOfWork unitOfWork;

        public TripDataUtil(ITripRepository tripRepository, IUnitOfWork uniOfWork)
        {
            if (tripRepository == null)
            {
                throw new ArgumentNullException("tripRepository can not be null!");
            }

            if (uniOfWork == null)
            {
                throw new ArgumentNullException("uniOfWork can not be null!");
            }

            this.tripRepository = tripRepository;
            this.unitOfWork = uniOfWork;
        }

        public void AddTrip(ITrip trip)
        {
            this.tripRepository.Add(trip);
            this.unitOfWork.Commit();
        }

        public ITrip GetTripById(int tripId)
        {
            var trip = this.tripRepository.GetById(tripId);

            return trip;
        }

        public IEnumerable<ITrip> GetTrips(int skip, int take)
        {
            var trips = this.tripRepository.GetTrips(skip, take);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take)
        {
            var trips = this.tripRepository.GetTripsByCategoryName(categoryName, skip, take);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending)
        {
            var trips = this.tripRepository.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsBySearchPattern(string pattern, int skip, int take)
        {
            var trips = this.tripRepository.GetTripsBySearchPattern(pattern, skip, take);

            return trips;
        }

        public int GetTripsCount()
        {
            var count = this.tripRepository.GetTripsCount();

            return count;
        }

        public int GetTripsCountByCategoryName(string categoryName)
        {
            var count = this.tripRepository.GetTripsCountByCategoryName(categoryName);

            return count;
        }

        public int GetTripsCountBySearchPattern(string pattern)
        {
            var count = this.tripRepository.GetTripsCountBySearchPattern(pattern);

            return count;
        }

        public void UpdateTrip(ITrip trip)
        {
            this.tripRepository.Update(trip);
            this.unitOfWork.Commit();
        }
    }
}
