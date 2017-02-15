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
        private const string USERNAME = "name";

        public event EventHandler<AddReviewEventArgs> AddReview;
        public event EventHandler<GetUserReviewsEventArgs> GetReviews;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Context.User.Identity.Name == this.Request.QueryString[USERNAME])
            {
                this.ButtonAddComment.Visible = false;
            }

            this.LoadData();
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            var content = this.TextBoxAddReviewText.Text;
            var rating = this.RadioButtonsRating.SelectedValue;
            var toUser = this.Request.QueryString["name"];
            var fromUser = this.Context.User.Identity.Name;

            this.AddReview?.Invoke(this, new AddReviewEventArgs() { FromUser = fromUser, ToUser = toUser, Content = content, Rating = rating });

            this.LoadData();
        }

        private void LoadData()
        {
            this.GetReviews?.Invoke(this, new GetUserReviewsEventArgs() { Username = this.Request.QueryString[USERNAME] });
            this.ListViewComments.DataSource = this.Model.Reviews;
            this.ListViewComments.DataBind();
        }
    }
}