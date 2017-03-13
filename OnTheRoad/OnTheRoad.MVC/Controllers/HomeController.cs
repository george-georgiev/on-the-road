using OnTheRoad.Logic.Contracts;
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
        private readonly ITripGetService tripsService;
        private readonly IUserGetService usersService;

        public HomeController(ITripGetService tripsService, IUserGetService usersService)
        {
            if (tripsService == null)
            {
                throw new ArgumentNullException("tripsService cannot be null!");
            }

            if (usersService == null)
            {
                throw new ArgumentNullException("usersService cannot be null!");
            }

            this.tripsService = tripsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var recentTrips = this.tripsService.GetTrips(0, RecentTripsCount);
            var usersCount = this.usersService.GetAllUsersCount();
            var tripsCount = this.tripsService.GetTripsCount();

            var model = new HomeViewModel() { Trips = recentTrips, AllUsersCount = usersCount, AllTripsCount = tripsCount };

            return this.View(model);
        }
    }
}