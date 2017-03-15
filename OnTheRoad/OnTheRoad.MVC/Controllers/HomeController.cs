using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class HomeController : Controller
    {
        private const int RecentTripsCount = 4;
        private readonly ITripGetService tripService;
        private readonly IUserGetService userService;

        public HomeController(ITripGetService tripService, IUserGetService userService)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService cannot be null!");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService cannot be null!");
            }

            this.tripService = tripService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var recentTrips = this.tripService.GetTrips(0, RecentTripsCount);
            var mappedTrips = new List<TripViewModel>();
            foreach (var trip in recentTrips)
            {
                var mapper = MapperProvider.Mapper;
                var mappedTrip = mapper.Map<TripViewModel>(trip);
                mappedTrips.Add(mappedTrip);
            }

            var usersCount = this.userService.GetAllUsersCount();
            var tripsCount = this.tripService.GetTripsCount();

            var model = new HomeViewModel() { Trips = mappedTrips, AllUsersCount = usersCount, AllTripsCount = tripsCount };

            return this.View(model);
        }
    }
}