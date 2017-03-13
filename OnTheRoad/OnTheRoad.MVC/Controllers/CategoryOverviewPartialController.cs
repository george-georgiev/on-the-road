using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class CategoryOverviewPartialController : Controller
    {
        private const int TripsCount = 4;

        private readonly ITripGetService tripGetService;

        public CategoryOverviewPartialController(ITripGetService tripGetService)
        {
            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService cannot be null!");
            }

            this.tripGetService = tripGetService;
        }

        public ActionResult Index(string categoryName)
        {
            var mappedTrips = new List<TripViewModel>();
            var trips = this.tripGetService.GetTripsByCategoryNameOrderedByDate(categoryName, TripsCount);
            foreach (var trip in trips)
            {
                var mappedTrip = MapperProvider.Mapper.Map<TripViewModel>(trip);
                mappedTrips.Add(mappedTrip);
            }

            var categoryOverviewModel = new CategoryOverviewViewModel();
            categoryOverviewModel.Name = categoryName;
            categoryOverviewModel.Trips = mappedTrips;

            return this.PartialView("_CategoryOverviewPartial", categoryOverviewModel);
        }
    }
}