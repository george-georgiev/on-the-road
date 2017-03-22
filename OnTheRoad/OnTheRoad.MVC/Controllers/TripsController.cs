using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.Infrastructure.Json;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Extensions;
using OnTheRoad.MVC.Filters;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class TripsController : Controller
    {
        private const int Take = 3;

        private readonly ITripService tripService;
        private readonly ISubscriptionService subscriptionService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imageService;
        private readonly ITripBuilder tripBuilder;

        public TripsController(
            ITripService tripService,
            ISubscriptionService subscriptionService,
            ICategoryService categoryService,
            IImageService imageService,
            ITripBuilder tripBuilder)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService cannot be null!");
            }

            if (subscriptionService == null)
            {
                throw new ArgumentNullException("subscriptionAddService can not be null!");
            }

            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService can not be null!");
            }

            if (imageService == null)
            {
                throw new ArgumentNullException("imageService cannot be null!");
            }

            if (tripBuilder == null)
            {
                throw new ArgumentNullException("tripBuilder cannot be null!");
            }

            this.tripService = tripService;
            this.subscriptionService = subscriptionService;
            this.categoryService = categoryService;
            this.imageService = imageService;
            this.tripBuilder = tripBuilder;
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
                var trips = this.tripService.GetTripsBySearchPattern(pattern, skip, Take);
                var mappedTrips = MapperProvider.Mapper.Map<IEnumerable<TripViewModel>>(trips);

                var total = this.tripService.GetTripsCountBySearchPattern(pattern);

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
            var trip = this.tripService.GetTripById(id);

            var model = MapperProvider.Mapper.Map<TripDetailsViewModel>(trip);

            var userName = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var isOrganiser = userName == trip.Organiser.Username;
            var isAvailable = ControllerUtilProvider.ControllerUtil.IsAuthenticated && !isOrganiser;
            model.CanSubscribe = isAvailable;

            var subscriptionStatus = this.subscriptionService.GetUserSubscriptionStatus(trip, userName);
            model.IsNone = subscriptionStatus == SubscriptionStatus.None;
            model.IsAttending = subscriptionStatus == SubscriptionStatus.Attending;
            model.IsInterested = subscriptionStatus == SubscriptionStatus.Interested;

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [Ajax]
        public ActionResult Subscribe(int tripId, string statusValue)
        {
            Result result;
            try
            {
                var status = (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), statusValue);
                var userName = ControllerUtilProvider.ControllerUtil.LoggedUserName;
                this.subscriptionService.AddOrUpdateSubscription(userName, tripId, status);

                result = new Result(Resources.Messages.SubscriptionSuccess, ResponseStatus.Ok);
            }
            catch (Exception)
            {
                result = new Result(Resources.Messages.SubscriptionError, ResponseStatus.ServerError);
                ControllerUtilProvider.ControllerUtil.SetResponseStatusCode(ResponseStatus.ServerError);
            }

            return this.Json(result);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddTrip()
        {
            var categories = this.categoryService.GetAllCategories();
            var listItems = new List<SelectListItem>();
            foreach (var category in categories)
            {
                var listItem = new SelectListItem() { Text = category.Name, Value = category.Id.ToString() };
                listItems.Add(listItem);
            }

            var model = new AddTripViewModel();
            model.AllCategories = listItems;

            var image = this.imageService.LoadTripsImage();
            model.CoverImage = image;

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddTrip(AddTripViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var name = model.Name;
            var description = model.Description;
            var location = model.Location;
            var startDate = model.StartDate;
            var endDate = model.EndDate;
            var coverImage = model.CoverImage;

            var trip = this.tripBuilder
                .SetName(name)
                .SetDescription(description)
                .SetLocation(location)
                .SetStartDate(startDate)
                .SetEndDate(endDate)
                .SetImage(coverImage)
                .Build();

            var loggedUsername = ControllerUtilProvider.ControllerUtil.LoggedUserName;

            var categoryIds = model.CategoryIds.Select(int.Parse);
            var tagNames = model.TagNames.Distinct().Select(x => x.Trim());

            this.tripService.AddTrip(trip, loggedUsername, categoryIds, tagNames);

            this.AddToastMessage(string.Empty, Resources.Messages.AddTripSuccess, ToastType.Success);
            return this.RedirectToAction("Index", "Home");
        }
    }
}