﻿using OnTheRoad.Logic.Contracts;
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

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName)
        {
            var trips = this.tripRepository.GetTripsByCategoryName(categoryName);

            return trips;
        }

        public IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending)
        {
            var trips = this.tripRepository.GetTripsOrderedByDateCreated(count, isAscending);

            return trips;
        }
    }
}