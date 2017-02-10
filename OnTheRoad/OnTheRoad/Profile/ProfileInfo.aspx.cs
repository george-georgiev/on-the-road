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
using OnTheRoad.Domain.Models;
using System.Linq;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        private const string USERNAME = "name";
        private const string FAVOURITE_USERS = "favouriteUsers";

        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;
        public event EventHandler<ProfileInfoEventArgs> UpdateProfileInfo;
        public event EventHandler<FavouriteUserEventArgs> RemoveFavouriteUser;
        public event EventHandler<FavouriteUserEventArgs> AddFavouriteUser;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs() { Username = this.Request.QueryString[USERNAME] });
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };

            this.RepeaterFavouriteUsers.DataSource = this.Model.FavouriteUsers;
            this.Page.DataBind();

            // add users to sesstion
            if (this.Request.QueryString[USERNAME] == this.Context.User.Identity.Name)
            {
                this.Session.Add(FAVOURITE_USERS, this.Model.FavouriteUsers);
                this.ButtonEdit.Visible = true;
            }

            // if on different user page -> show or hide follow and unfollow btns
            if (this.Context.User.Identity.Name != string.Empty && 
                this.Context.User.Identity.Name != this.Request.QueryString[USERNAME])
            {
             

                IEnumerable<IUser> favouriteUsers = this.Session[FAVOURITE_USERS] as IEnumerable<IUser>;
                if (favouriteUsers != null)
                {
                    var isFollowing = favouriteUsers.Any(x => x.Username == this.Request.QueryString[USERNAME]);

                    if (isFollowing)
                    {
                        this.UpdatePanelUnfollow.Visible = true;
                    }
                    else
                    {
                        this.UpdatePanelFollow.Visible = true;
                    }
                }
                else
                {
                    this.UpdatePanelFollow.Visible = true;
                }
            }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            this.FormViewProfileInfo.ChangeMode(FormViewMode.Edit);
            this.ButtonEdit.Visible = false;
            this.PanelFavouriteUsers.Visible = false;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
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
            this.ButtonEdit.Visible = true;
            this.PanelFavouriteUsers.Visible = true;
        }

        protected void ButtonUnfollow_Click(object sender, EventArgs e)
        {
            var favUserToRemove = ((Button)sender).CommandArgument;
            this.RemoveFavouriteUser?.Invoke(this, new FavouriteUserEventArgs()
            {
                FavouriteUserUsername = favUserToRemove,
                CurrentUserUsername = this.Context.User.Identity.Name
            });

            //this.UpdatePanelUnfollow.Visible = false;
            //this.UpdatePanelFollow.Visible = true;
            //this.Session[FAVOURITE_USERS];
        }

        protected void ButtonFollow_Click(object sender, EventArgs e)
        {
            this.AddFavouriteUser?.Invoke(this, new FavouriteUserEventArgs()
            {
                CurrentUserUsername = this.Context.User.Identity.Name,
                FavouriteUserUsername = this.Request.QueryString[USERNAME]
            });

            //this.UpdatePanelUnfollow.Visible = true;
            //this.UpdatePanelFollow.Visible = false;
            //this.Session[FAVOURITE_USERS] = this.Model.FavouriteUsers;
        }
    }
}