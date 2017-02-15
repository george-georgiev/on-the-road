using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Models;
using System.Linq;

namespace OnTheRoad.Logic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IGetUserService userService;
        private readonly IRatingService ratingService;
        private readonly IReviewRepository reviewRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReviewService(IGetUserService userService, IRatingService ratingService, IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService can not be null!");
            }

            if (ratingService == null)
            {
                throw new ArgumentNullException("ratingService can not be null!");
            }

            if (reviewRepository == null)
            {
                throw new ArgumentNullException("reviewRepository can not be null!");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("uniOfWork can not be null!");
            }

            this.userService = userService;
            this.ratingService = ratingService;
            this.reviewRepository = reviewRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddUserReview(string content, string fromUser, string toUser, string rating)
        {
            var r = this.ratingService.GetRatingByValue(rating);
            var fUser = this.userService.GetUserInfo(fromUser);
            var tUser = this.userService.GetUserInfo(toUser);
            var review = new Review(content, fUser, tUser, r);
            this.reviewRepository.Add(review);

            this.unitOfWork.Commit();
        }

        public IEnumerable<IReview> GetUserReviews(string username)
        {
            var reviews = this.reviewRepository.GetByToUser(username);
            return reviews;
        }
    }
}
