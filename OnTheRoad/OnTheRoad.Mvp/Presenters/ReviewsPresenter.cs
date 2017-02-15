using System;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Views;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class ReviewsPresenter : Presenter<IReviewsView>
    {
        private readonly IReviewService reviewService;

        public ReviewsPresenter(IReviewsView view, IReviewService reviewService)
            : base(view)
        {
            if (reviewService == null)
            {
                throw new ArgumentNullException("reviewService cannot be null.");
            }

            this.reviewService = reviewService;

            this.View.AddReview += View_AddReview;
            this.View.GetReviews += View_GetReviews;
        }

        private void View_GetReviews(object sender, GetUserReviewsEventArgs e)
        {
            this.View.Model.Reviews = this.reviewService.GetUserReviews(e.Username);
        }

        private void View_AddReview(object sender, AddReviewEventArgs e)
        {
            this.reviewService.AddUserReview(e.Content, e.FromUser, e.ToUser, e.Rating, e.PostingDate);
        }
    }
}
