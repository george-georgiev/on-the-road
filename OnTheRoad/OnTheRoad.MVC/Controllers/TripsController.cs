using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.Infrastructure.Json;
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
        private readonly ISubscriptionService subscriptionService;

        public TripsController(ITripGetService tripGetService, ISubscriptionService subscriptionService)
        {
            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService cannot be null!");
            }

            if (subscriptionService == null)
            {
                throw new ArgumentNullException("subscriptionAddService can not be null!");
            }

            this.tripGetService = tripGetService;
            this.subscriptionService = subscriptionService;
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
        public ActionResult Details(int id)
        {
            var trip = this.tripGetService.GetTripById(id);

            var model = MapperProvider.Mapper.Map<TripDetailsViewModel>(trip);

            var userName = this.User.Identity.Name;
            var isOrganiser = userName == trip.Organiser.Username;
            var isAvailable = this.Request.IsAuthenticated && !isOrganiser;
            model.CanSubscribe = isAvailable;

            var subscriptionStatus = this.subscriptionService.GetUserSubscriptionStatus(trip, userName);
            model.IsNone = subscriptionStatus == SubscriptionStatus.None;
            model.IsAttending = subscriptionStatus == SubscriptionStatus.Attending;
            model.IsInterested = subscriptionStatus == SubscriptionStatus.Interested;

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Subscribe(int tripId, string statusValue)
        {
            if (!this.Request.IsAjaxRequest())
            {
                throw new InvalidOperationException("Ajax request is required!");
            }

            Result result;
            try
            {
                var status = (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), statusValue);
                var userName = this.User.Identity.Name;
                this.subscriptionService.AddOrUpdateSubscription(userName, tripId, status);

                result = new Result(Resources.Messages.SubscriptionSuccess, ResponseStatus.Success);
            }
            catch (Exception)
            {
                result = new Result(Resources.Messages.SubscriptionError, ResponseStatus.Error);
            }

            return this.Json(result);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddTrip()
        {
            return this.View();
        }
    }
}