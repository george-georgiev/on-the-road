using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Factories;

namespace OnTheRoad.Logic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewAddHelper reviewAddHelper;
        private readonly IReviewDataUtils reviewDataUtils;
        private readonly IReviewFactory reviewFactory;

        public ReviewService(IReviewAddHelper reviewAddHelper, IReviewDataUtils reviewDataUtils, IReviewFactory reviewFactory)
        {
            if (reviewAddHelper == null)
            {
                throw new ArgumentNullException("reviewAddHelper can not be null!");
            }

            if (reviewDataUtils == null)
            {
                throw new ArgumentNullException("reviewDataUtils can not be null!");
            }

            if (reviewFactory == null)
            {
                throw new ArgumentNullException("reviewFactory can not be null!");
            }

            this.reviewAddHelper = reviewAddHelper;
            this.reviewDataUtils = reviewDataUtils;
            this.reviewFactory = reviewFactory;
        }

        public void AddUserReview(string content, string fromUser, string toUser, string rating, DateTime postingDate)
        {
            var givenRating = this.reviewAddHelper.GetRatingByValue(rating);
            var fUser = this.reviewAddHelper.GetUserByUsername(fromUser);
            var tUser = this.reviewAddHelper.GetUserByUsername(toUser);
            var review = this.reviewFactory.CreateReview(content, fUser, tUser, givenRating, postingDate);

            this.reviewDataUtils.AddUserReview(review);
        }

        public IEnumerable<IReview> GetUserReviews(string username)
        {
            return this.reviewDataUtils.GetUserReviews(username);
        }
    }
}
