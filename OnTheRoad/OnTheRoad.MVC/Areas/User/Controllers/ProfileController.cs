using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.Infrastructure.Json;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Areas.User.Models;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.User.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICityService cityService;
        private readonly IUserService userService;

        public ProfileController(IUserService userService, ICityService cityService)
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
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string username)
        {
            var user = this.userService.GetUserInfo(username);
            var mappedUser = MapperProvider.Mapper.Map<UserViewModel>(user);

            var model = new ProfileViewModel();
            model.User = mappedUser;

            var loggedUserName = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var loggedUser = this.userService.GetUserInfo(loggedUserName);
            var favouriteUsers = loggedUser.FavouriteUsers;
            var isFollowing = favouriteUsers.Any(x => x.Username == username);
            model.IsFollowing = isFollowing;

            model.IsOwner = loggedUserName == username;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Ajax]
        public ActionResult Update(UserViewModel user)
        {
            var loggedUserName = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            if (loggedUserName != user.Username)
            {
                ControllerUtilProvider.ControllerUtil.SetResponseStatusCode(ResponseStatus.BadRequest);

                return this.Json(new Result(Resources.Messages.UnAuthorizedAccess, ResponseStatus.BadRequest));
            }

            Result result;
            try
            {
                var username = user.Username;
                var cityId = user.CityId;
                var city = this.cityService.GetCityById(cityId);

                var firstName = user.FirstName;
                var lastName = user.LastName;
                var phoneNumber = user.PhoneNumber;
                var info = user.Info;
                var image = user.Image;
                this.userService.UpdateUserInfo(username, firstName, lastName, phoneNumber, info, image, city);

                result = new Result(Resources.User.Messages.UpdateUserSuccess, ResponseStatus.Ok);
            }
            catch (Exception)
            {
                result = new Result(Resources.User.Messages.UpdateUserError, ResponseStatus.ServerError);
                ControllerUtilProvider.ControllerUtil.SetResponseStatusCode(ResponseStatus.ServerError);
            }

            return this.Json(result);
        }

        [Authorize]
        [HttpPost]
        [Ajax]
        public ActionResult Follow(string username)
        {
            var result = this.UpdateFollowing(username, true, Resources.User.Messages.FollowSucces, Resources.User.Messages.FollowError);

            return result;
        }

        [Authorize]
        [HttpPost]
        [Ajax]
        public ActionResult Unfollow(string username)
        {
            var result = this.UpdateFollowing(username, false, Resources.User.Messages.UnfollowSucces, Resources.User.Messages.UnfollowError);

            return this.Json(result);
        }

        private ActionResult UpdateFollowing(string username, bool isFollow, string successMessage, string errorMessage)
        {
            var loggedUserName = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            if (loggedUserName == username)
            {
                ControllerUtilProvider.ControllerUtil.SetResponseStatusCode(ResponseStatus.BadRequest);

                return this.Json(new Result(Resources.Messages.InvalidRequest, ResponseStatus.BadRequest));
            }

            Result result;
            try
            {
                if (isFollow)
                {
                    this.userService.AddFavouriteUser(loggedUserName, username);
                }
                else
                {
                    this.userService.RemoveFavouriteUser(loggedUserName, username);
                }

                result = new Result(successMessage, ResponseStatus.Ok);
            }
            catch (Exception)
            {
                result = new Result(errorMessage, ResponseStatus.ServerError);
                ControllerUtilProvider.ControllerUtil.SetResponseStatusCode(ResponseStatus.ServerError);
            }

            return this.Json(result);
        }
    }
}