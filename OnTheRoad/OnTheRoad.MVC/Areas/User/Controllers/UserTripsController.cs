using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.User.Controllers
{
    public class UserTripsController : Controller
    {
        private const int Take = 3;

        private readonly ITripGetService tripService;

        public UserTripsController(ITripGetService tripService)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService can not be null!");
            }

            this.tripService = tripService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Attending(string username, int page = 1)
        {
            page = page > 0 ? page : 1;
            var skip = (page - 1) * Take;
            var trips = this.tripService.GetUserAttendingTrips(username, skip, Take);

            var mappedTrips = MapperProvider.Mapper.Map<IEnumerable<TripViewModel>>(trips);

            var total = this.tripService.GetUserAttendingTripsCount(username);

            var model = new TripsWithPagingViewModel();
            model.Heading = $"{Resources.Labels.Attending}:";
            model.PageHyperLink = $"/user/trips/attending/{username}/";
            model.Page = page;
            model.Trips = mappedTrips;
            model.Total = total;
            model.Take = Take;

            return View("_TripsWithPaging", model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Interested(string username, int page = 1)
        {
            return View();
        }
    }
}