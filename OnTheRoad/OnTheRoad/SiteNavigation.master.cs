using System;
using System.Web.UI;

namespace OnTheRoad
{
    public partial class SiteNavigation : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButtonSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxSearch.Text) && !string.IsNullOrWhiteSpace(this.TextBoxSearch.Text))
            {
                this.Response.Redirect($"/trips/search/{this.TextBoxSearch.Text}");
            }
        }
    }
}