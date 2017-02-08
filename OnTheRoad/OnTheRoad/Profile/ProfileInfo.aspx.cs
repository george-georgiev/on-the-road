using System;
using WebFormsMvp;
using WebFormsMvp.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Profile.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs());
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };
            this.FormViewProfileInfo.DataBind();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            this.FormViewProfileInfo.ChangeMode(FormViewMode.Edit);
            this.EditButton.Visible = false;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            TextBox firstName = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;
            TextBox lastName = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;
            TextBox city = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;
            TextBox country = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;
            TextBox username = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;


            this.FormViewProfileInfo.ChangeMode(FormViewMode.ReadOnly);
            this.EditButton.Visible = true;
        }
    }
}