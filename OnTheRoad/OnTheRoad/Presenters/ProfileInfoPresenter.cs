using System;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Profile.Contracts;
using WebFormsMvp;
using OnTheRoad.Common;

namespace OnTheRoad.Presenters
{
    public class ProfileInfoPresenter : Presenter<IProfileInfoView>
    {
        private readonly IUserService userService;

        public ProfileInfoPresenter(IProfileInfoView view, IUserService userService)
            : base(view)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService cannot be null.");
            }

            this.userService = userService;
            this.View.GetProfileInfo += View_GetProfileInfo;
        }

        private void View_GetProfileInfo(object sender, ProfileInfoEventArgs e)
        {
            var userId = UserInfoUtility.GetCurrentUserId(this.HttpContext.User.Identity);
            var user = this.userService.GetUserInfo(userId);

            this.View.Model.Username = user.Username;
            this.View.Model.FirstName = user.FirstName;
            this.View.Model.LastName = user.LastName;
            this.View.Model.Email = user.Email;
            this.View.Model.City = user.City != null ? user.City.Name : string.Empty;
            this.View.Model.Country = user.Country != null ? user.Country.Name : string.Empty;
            this.View.Model.PhoneNumber = user.PhoneNumber != null ? user.PhoneNumber : string.Empty;
            this.View.Model.Info = user.Info != null ? user.Info : string.Empty;
            //this.View.Model.ImagePath = user.Image.Path;
            //this.View.Model.FavouriteUsers = user.FavouriteUsers;
        }
    }
}