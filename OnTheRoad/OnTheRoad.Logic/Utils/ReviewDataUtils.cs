using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Logic.Utils
{
    public class ReviewDataUtils : IReviewDataUtils
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IUnitOfWork unitOfWork;
        
        public ReviewDataUtils(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            if (reviewRepository == null)
            {
                throw new ArgumentNullException("reviewRepository cannot be null!");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork cannot be null!");
            }

            this.reviewRepository = reviewRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddUserReview(IReview review)
        {
            this.reviewRepository.Add(review);
            this.unitOfWork.Commit();
        }

        public IEnumerable<IReview> GetUserReviews(string username)
        {
            var reviews = this.reviewRepository.GetByToUser(username);

            return reviews;
        }

        public IEnumerable<IReview> GetUserReviews(string username, int skip, int take)
        {
            var reviews = this.reviewRepository.GetByToUser(username, skip, take);

            return reviews;
        }

        public int GetUserReviewsTotal(string username)
        {
            var total = this.reviewRepository.GetByToUserTotal(username);

            return total;
        }
    }
}
