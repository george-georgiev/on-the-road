using System;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Utils
{
    public class ReviewAddHelper : IReviewAddHelper
    {
        private readonly IGetUserService userService;
        private readonly IRatingService ratingService;

        public ReviewAddHelper(IGetUserService userService, IRatingService ratingService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService can not be null!");
            }

            if (ratingService == null)
            {
                throw new ArgumentNullException("ratingService can not be null!");
            }

            this.userService = userService;
            this.ratingService = ratingService;
        }

        public IRating GetRatingByValue(RatingEnum value)
        {
            return this.ratingService.GetRatingByValue(value);
        }

        public IUser GetUserByUsername(string username)
        {
            return this.userService.GetUserInfo(username);
        }
    }
}
