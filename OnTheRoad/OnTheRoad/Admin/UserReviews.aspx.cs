using System;
using System.Web.UI.WebControls;

namespace OnTheRoad.Admin
{
    public partial class UserReviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridViewReviews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = this.GridViewReviews.EditIndex;
            GridViewRow row = this.GridViewReviews.Rows[index];
            string reviewId = (row.FindControl("LiteralReviewId") as Literal).Text;
            bool checkBox = (row.FindControl("CheckBoxIsDeleted") as CheckBox).Checked;

            this.ObjectDataSourceReviews.UpdateParameters.Add("IsDeleted", checkBox.ToString());
            this.ObjectDataSourceReviews.UpdateParameters.Add("ReviewId", reviewId);
        }
    }
}