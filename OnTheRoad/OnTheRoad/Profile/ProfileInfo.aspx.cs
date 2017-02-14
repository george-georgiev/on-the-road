using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using ImageResizer;
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
        private const string FAVOURITE_USERS = "favouriteUsers";

        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;
        public event EventHandler<ProfileInfoEventArgs> UpdateProfileInfo;
        public event EventHandler<FavouriteUserEventArgs> RemoveFavouriteUser;
        public event EventHandler<FavouriteUserEventArgs> AddFavouriteUser;
        public event EventHandler<ProfileImageEventArgs> UpdateProfileImage;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs() { Username = this.Request.QueryString[USERNAME] });
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };

            this.RepeaterFavouriteUsers.DataSource = this.Model.FavouriteUsers;
            this.Page.DataBind();

            // Add users to sesstion.
            if (this.Request.QueryString[USERNAME] == this.Context.User.Identity.Name)
            {
                var favUsers = this.Model.FavouriteUsers.Select(x => x.Username).ToList();
                this.Session.Add(FAVOURITE_USERS, favUsers);
            }

            // If on different user page -> show or hide follow and unfollow buttons.
            if (this.Context.User.Identity.Name != string.Empty &&
                this.Context.User.Identity.Name != this.Request.QueryString[USERNAME])
            {
                this.ButtonEdit.Visible = false;

                IEnumerable<string> favouriteUsers = this.Session[FAVOURITE_USERS] as IEnumerable<string>;
                if (favouriteUsers != null)
                {
                    var isFollowing = favouriteUsers.Any(x => x == this.Request.QueryString[USERNAME]);

                    if (isFollowing)
                    {
                        this.ButtonUnfollow.Visible = true;
                    }
                    else
                    {
                        this.ButtonFollow.Visible = true;
                    }
                }
                else
                {
                    this.ButtonFollow.Visible = true;
                }
            }

            this.UpdatePanelFollowingButtons.Update();
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
            var favUserToRemove = this.Request.QueryString[USERNAME];
            this.RemoveFavouriteUser?.Invoke(this, new FavouriteUserEventArgs()
            {
                FavouriteUserUsername = favUserToRemove,
                CurrentUserUsername = this.Context.User.Identity.Name
            });

            this.RemoveFavouriteUserFromSession(favUserToRemove);
        }

        protected void DropdownUnfollow_Click(object sender, EventArgs e)
        {
            var favUserToRemove = ((Button)sender).CommandArgument;
            this.RemoveFavouriteUser?.Invoke(this, new FavouriteUserEventArgs()
            {
                FavouriteUserUsername = favUserToRemove,
                CurrentUserUsername = this.Context.User.Identity.Name
            });

            this.RemoveFavouriteUserFromSession(favUserToRemove);
        }

        protected void ButtonFollow_Click(object sender, EventArgs e)
        {
            this.AddFavouriteUser?.Invoke(this, new FavouriteUserEventArgs()
            {
                CurrentUserUsername = this.Context.User.Identity.Name,
                FavouriteUserUsername = this.Request.QueryString[USERNAME]
            });

            ICollection<string> favouriteUsers = this.Session[FAVOURITE_USERS] as ICollection<string>;
            favouriteUsers.Add(this.Request.QueryString[USERNAME]);
            this.Session[FAVOURITE_USERS] = favouriteUsers;
        }

        private void RemoveFavouriteUserFromSession(string favUserToRemove)
        {
            ICollection<string> favouriteUsers = this.Session[FAVOURITE_USERS] as ICollection<string>;
            var userToRemoveFromSession = favouriteUsers.First(x => x == favUserToRemove);
            favouriteUsers.Remove(userToRemoveFromSession);
            this.Session[FAVOURITE_USERS] = favouriteUsers;
        }

        protected void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            var fileUpload = this.FormViewProfileInfo.FindControl("FileUploadImage") as FileUpload;

            if (fileUpload.HasFile)
            {
                try
                {
                    if (fileUpload.PostedFile.ContentType == "image/jpeg" || fileUpload.PostedFile.ContentType == "image/png")
                    {
                        if (fileUpload.PostedFile.ContentLength < 4 * 1000 * 1024)
                        {
                            string filename = Path.GetFileName(fileUpload.FileName);
                            Stream fileStream = fileUpload.PostedFile.InputStream;
                            byte[] imageAsByteArray = null;

                            using (var ms = new MemoryStream())
                            {
                                ImageJob i = new ImageJob(fileStream, ms,
                                    new ImageResizer.Instructions("width=600;format=jpg;mode=max"));
                                i.Build();
                                imageAsByteArray = ms.ToArray();
                            }

                            this.UpdateProfileImage?.Invoke(this, new ProfileImageEventArgs()
                            {
                                Image = imageAsByteArray,
                                UserName = this.Context.User.Identity.Name
                            });
                           
                            this.LabelErrors.Text = "";
                        }
                        else
                        {
                            this.LabelErrors.Text = "Снимката трябва да е до 4MB!";
                        }
                    }
                    else
                    {
                        this.LabelErrors.Text = "Само JPEG И PNG файлове може да бъдат качвани!";
                    }
                }
                catch (Exception)
                {
                    this.LabelErrors.Text = "Възникна грешка при качването. Моля опитайте отново.";
                }
            }
        }
    }
}