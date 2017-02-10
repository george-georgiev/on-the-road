using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Profile.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.CustomControllers;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        private const string USERNAME = "name";

        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;
        public event EventHandler<ProfileInfoEventArgs> UpdateProfileInfo;
        public event EventHandler<FavouriteUserEventArgs> RemoveFavouriteUser;

        public string GetEmail { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs() { Username = this.Request.QueryString[USERNAME] });
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };

            if (this.GetEmail == null)
            {
                this.GetEmail = this.Model.Email;
            }

            this.RepeaterFavouriteUsers.DataSource = this.Model.FavouriteUsers;
            this.Page.DataBind();

            if (this.Request.QueryString["name"] != this.Context.User.Identity.Name)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            this.FormViewProfileInfo.ChangeMode(FormViewMode.Edit);
            this.EditButton.Visible = false;
            this.PanelFavouriteUsers.Visible = false;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            TextBox firstName = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;
            TextBox lastName = this.FormViewProfileInfo.FindControl("Lastname") as TextBox;
            CitiesDropDown city = this.FormViewProfileInfo.FindControl("City") as CitiesDropDown;
            TextBox email = this.FormViewProfileInfo.FindControl("Email") as TextBox;
            TextBox phoneNumber = this.FormViewProfileInfo.FindControl("PhoneNumber") as TextBox;
            TextBox info = this.FormViewProfileInfo.FindControl("Info") as TextBox;

            if (city.SelectedCityId == 0)
            {
                city.SelectedCityId = 1;
            }

            this.UpdateProfileInfo?.Invoke(this, new ProfileInfoEventArgs()
            {
                CityId = city.SelectedCityId,
                FirstName = firstName.Text,
                LastName = lastName.Text,
                PhoneNumber = phoneNumber.Text,
                Info = info.Text,
                Username = this.Request.QueryString[USERNAME]
            });

            this.FormViewProfileInfo.ChangeMode(FormViewMode.ReadOnly);
            this.EditButton.Visible = true;
            this.PanelFavouriteUsers.Visible = true;
        }

        protected void ButtonUnfollow_Click(object sender, EventArgs e)
        {
            var favUserToRemove = ((Button)sender).CommandArgument;
            this.RemoveFavouriteUser?.Invoke(this, new FavouriteUserEventArgs()
            {
                FavouriteUserToRemove = favUserToRemove,
                CurrentUsername = this.Request.QueryString[USERNAME]
            });
        }
    }
}