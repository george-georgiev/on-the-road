using System;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using OnTheRoad.Presenters;
using OnTheRoad.Profile.Contracts;
using WebFormsMvp;
using WebFormsMvp.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs());
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };
            this.FormViewProfileInfo.DataBind();

            
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            this.FormViewProfileInfo.ChangeMode(FormViewMode.Edit);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {

            TextBox firstName = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;

            var a = firstName.Text;
            this.FormViewProfileInfo.ChangeMode(FormViewMode.ReadOnly);
        }
    }
}