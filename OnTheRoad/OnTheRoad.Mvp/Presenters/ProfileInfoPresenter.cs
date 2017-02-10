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
        }

        private void View_UpdateProfileInfo(object sender, ProfileInfoEventArgs e)
        {
            var username = this.Request.QueryString["name"];
            var user = GetUserFromQueryString();
            var city = this.cityService.GetCityById(e.CityId);

            this.userService.UpdateUserInfo(user, e.FirstName, e.LastName, e.PhoneNumber, e.Info, city);
        }

        private void View_GetProfileInfo(object sender, ProfileInfoEventArgs e)
        {
            //var userId = UserInfoUtility.GetCurrentUserId(this.HttpContext.User.Identity);
            var user = GetUserFromQueryString();

            this.View.Model.FavouriteUsers = user.FavouriteUsers;
            this.View.Model.Username = user.Username;
            this.View.Model.FirstName = user.FirstName;
            this.View.Model.LastName = user.LastName;
            this.View.Model.Email = user.Email;
            this.View.Model.City = user.City != null ? user.City.Name : string.Empty;
            this.View.Model.PhoneNumber = user.PhoneNumber != null ? user.PhoneNumber : string.Empty;
            this.View.Model.Info = user.Info != null ? user.Info : string.Empty;
            this.View.Model.ImagePath = "http://klassa.bg/images/pictures/class_bg/img_47303.jpg";
            //this.View.Model.ImagePath = user.Image.Path;
        }

        private IUser GetUserFromQueryString()
        {
            var username = this.Request.QueryString["name"];
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