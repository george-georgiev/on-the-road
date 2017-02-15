using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface IReviewsView: IView<ReviewsModel>
    {
        event EventHandler<GetUserReviewsEventArgs> GetReviews;

        event EventHandler<AddReviewEventArgs> AddReview;
    }
}
