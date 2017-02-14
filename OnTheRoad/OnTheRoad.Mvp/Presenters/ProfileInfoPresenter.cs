using System;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Profile.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class ProfileInfoPresenter : Presenter<IProfileInfoView>
    {
        private readonly IUserService userService;
        private readonly ICityService cityService;

        public ProfileInfoPresenter(IProfileInfoView view, IUserService userService, ICityService cityService)
            : base(view)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService cannot be null.");
            }

            if (cityService == null)
            {
                throw new ArgumentNullException("cityService cannot be null.");
            }

            this.userService = userService;
            this.cityService = cityService;
            this.View.GetProfileInfo += View_GetProfileInfo;
            this.View.UpdateProfileInfo += View_UpdateProfileInfo;
            this.View.RemoveFavouriteUser += View_RemoveFavouriteUser;
            this.View.AddFavouriteUser += View_AddFavouriteUser;
            this.View.UpdateProfileImage += View_UpdateProfileImage;
        }

        private void View_UpdateProfileImage(object sender, ProfileImageEventArgs e)
        {
            this.userService.UpdateImage(e.Image, e.UserName);
        }

        private void View_AddFavouriteUser(object sender, FavouriteUserEventArgs e)
        {
            this.userService.AddFavouriteUser(e.CurrentUserUsername, e.FavouriteUserUsername);
        }

        private void View_RemoveFavouriteUser(object sender, FavouriteUserEventArgs e)
        {
            this.userService.RemoveFavouriteUser(e.CurrentUserUsername, e.FavouriteUserUsername);
        }

        private void View_UpdateProfileInfo(object sender, ProfileInfoEventArgs e)
        {
            var username = e.Username;
            var city = this.cityService.GetCityById(e.CityId);

            this.userService.UpdateUserInfo(username, e.FirstName, e.LastName, e.PhoneNumber, e.Info, city);
        }

        private void View_GetProfileInfo(object sender, ProfileInfoEventArgs e)
        {
            var user = this.GetCurrentUser(e.Username);

            this.View.Model.FavouriteUsers = user.FavouriteUsers;
            this.View.Model.Username = user.Username;
            this.View.Model.FirstName = user.FirstName;
            this.View.Model.LastName = user.LastName;
            this.View.Model.Email = user.Email;
            this.View.Model.City = user.City != null ? user.City.Name : string.Empty;
            this.View.Model.PhoneNumber = user.PhoneNumber != null ? user.PhoneNumber : string.Empty;
            this.View.Model.Info = user.Info != null ? user.Info : string.Empty;
            this.View.Model.Image = user.Image;
            //this.View.Model.ImagePath = "http://klassa.bg/images/pictures/class_bg/img_47303.jpg";
            // TODO: Add real image from DB
            //this.View.Model.ImagePath = user.Image.Path;
        }

        private IUser GetCurrentUser(string username)
        {
            var user = this.userService.GetUserInfo(username);
            if (user == null)
            {
                this.Response.Redirect("http://localhost:52612/");
                return null;
            }
            else
            {
                return user;
            }
        }
    }
}