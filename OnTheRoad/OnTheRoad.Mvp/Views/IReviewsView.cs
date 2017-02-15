using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface IReviewsView: IView<ReviewsModel>
    {
        event EventHandler<ProfileReviewsEventArgs> GetReviews;

        event EventHandler<ProfileReviewsEventArgs> AddReview;
    }
}
