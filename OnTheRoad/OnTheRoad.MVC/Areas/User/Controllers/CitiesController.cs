using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Areas.User.Models;
using OnTheRoad.MVC.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.User.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService cityService;
        private readonly IUserGetService userService;

        public CitiesController(ICityService cityService, IUserGetService userService)
        {
            if (cityService == null)
            {
                throw new ArgumentNullException("cityService cannot be null!");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService cannot be null!");
            }

            this.cityService = cityService;
            this.userService = userService;
        }

        public ActionResult All()
        {
            var cities = this.cityService.GetAllCities();
            var mappedCities = MapperProvider.Mapper.Map<IEnumerable<CityViewModel>>(cities);

            var model = new CitiesAllViewModel();
            model.Cities = mappedCities;

            var loggedUserName = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var user = this.userService.GetUserInfo(loggedUserName);
            var cityName = user.City?.Name;

            model.SelectedCityName = cityName;

            return View("_CitiesAllPartial", model);
        }
    }
}