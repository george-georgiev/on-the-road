using System;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Logic.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            if (ratingRepository == null)
            {
                throw new ArgumentNullException("ratingRepository cannot be null!");
            }

            this.ratingRepository = ratingRepository;
        }

        public IRating GetRatingByValue(RatingEnum value)
        {
            return this.ratingRepository.GetByValue(value.ToString());
        }
    }
}
