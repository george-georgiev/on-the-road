using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IReviewDataUtils
    {
        void AddUserReview(IReview review);

        IEnumerable<IReview> GetUserReviews(string username);

        IEnumerable<IReview> GetUserReviews(string username, int skip, int take);

        int GetUserReviewsTotal(string username);
    }
}
