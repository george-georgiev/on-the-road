using System;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Factories
{
    public interface IReviewFactory
    {
        IReview CreateReview(string reviewContent, IUser fromUser, IUser toUser, IRating rating, DateTime postingDate);
    }
}
