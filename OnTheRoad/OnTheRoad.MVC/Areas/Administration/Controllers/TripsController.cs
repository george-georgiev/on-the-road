using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Areas.Administration.Models;
using OnTheRoad.MVC.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TripsController : Controller
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService can not be null!");
            }

            this.tripService = tripService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var tripsCount = this.tripService.GetTripsCount();
            var skip = 0;
            var trips = this.tripService.GetTrips(skip, tripsCount);

            var mapped = MapperProvider.Mapper.Map<IEnumerable<TripViewModel>>(trips);

            return this.View("TripsGrid", mapped);
        }

        [HttpPost]
        public ActionResult Update(TripViewModel model)
        {
            var trip = MapperProvider.Mapper.Map<ITrip>(model);
            this.tripService.Update(trip);

            return RedirectToAction("Index");
        }
    }
}