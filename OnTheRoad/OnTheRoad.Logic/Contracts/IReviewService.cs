using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Contracts
{
    public interface IReviewService
    {
        IEnumerable<IReview> GetUserReviews(string username);

        IEnumerable<IReview> GetUserReviews(string username, int skip, int take);
        
        void AddUserReview(string content, string fromUser, string toUser, RatingEnum rating, DateTime postingDate);

        int GetUserReviewsTotal(string username);
    }
}
