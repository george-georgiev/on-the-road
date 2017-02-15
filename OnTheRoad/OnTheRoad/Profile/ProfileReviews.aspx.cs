using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ReviewsPresenter))]
    public partial class ProfileReviews : MvpPage<ReviewsModel>, IReviewsView
    {
        public event EventHandler<ProfileReviewsEventArgs> AddReview;
        public event EventHandler<ProfileReviewsEventArgs> GetReviews;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            var content = this.TextBoxAddReviewText.Text;
            var rating = this.RadioButtonsRating.SelectedValue;
            var toUser = this.Request.QueryString["name"];
            var fromUser = this.Context.User.Identity.Name;

            this.AddReview?.Invoke(this, new ProfileReviewsEventArgs() { FromUser = fromUser, ToUser = toUser, Content = content, Rating = rating });
        }
    }
}