using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IReviewService
    {
        IEnumerable<IReview> GetUserReviews(string username);

        void AddUserReview(string content, string fromUser, string toUser, string rating, DateTime postingDate);
    }
}
