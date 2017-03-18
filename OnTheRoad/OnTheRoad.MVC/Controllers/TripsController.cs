using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class TripsController : Controller
    {
        private const int Take = 3;

        private readonly ITripGetService tripGetService;

        public TripsController(ITripGetService tripGetService)
        {
            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService cannot be null!");
            }

            this.tripGetService = tripGetService;

        }

        [HttpGet]
        public ActionResult Search(string pattern, int page = 1)
        {
            var categoryModel = new TripsWithPagingViewModel();
            categoryModel.Heading = $"{Resources.Labels.SearchResultsFor}: {pattern}";
            categoryModel.PageHyperLink = $"/trips/search/{pattern}/";

            if (string.IsNullOrEmpty(pattern) || string.IsNullOrWhiteSpace(pattern))
            {
                categoryModel.Trips = new List<TripViewModel>();
                categoryModel.Page = 1;
            }
            else
            {
                page = page > 0 ? page : 1;
                var skip = (page - 1) * Take;
                var trips = this.tripGetService.GetTripsBySearchPattern(pattern, skip, Take);
                var mappedTrips = MapperProvider.Mapper.Map<IEnumerable<TripViewModel>>(trips);

                var total = this.tripGetService.GetTripsCountBySearchPattern(pattern);

                categoryModel.Page = page;
                categoryModel.Trips = mappedTrips;
                categoryModel.Total = total;
                categoryModel.Take = Take;
            }

            return this.View("_TripsWithPaging", categoryModel);
        }

        [HttpGet]
        public ActionResult Details()
        {
            return this.View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddTrip()
        {
            return this.View();
        }
    }
}