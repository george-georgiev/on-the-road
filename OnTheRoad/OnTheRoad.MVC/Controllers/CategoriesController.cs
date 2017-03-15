﻿using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private const int Take = 3;

        private readonly ICategoryService categoryService;
        private readonly ITripGetService tripGetService;

        public CategoriesController(ICategoryService categoryService, ITripGetService tripGetService)
        {
            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService cannot be null!");
            }

            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService cannot be null!");
            }

            this.categoryService = categoryService;
            this.tripGetService = tripGetService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.categoryService.GetAllCategories();
            var categoriesModel = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                var mappedCategory = MapperProvider.Mapper.Map<CategoryViewModel>(category);
                categoriesModel.Add(mappedCategory);
            }

            return this.View(categoriesModel);
        }

        [HttpGet]
        public ActionResult Details(string categoryName, int page = 1)
        {
            page = page > 0 ? page : 1;
            var skip = (page - 1) * Take;
            var trips = this.GetTrips(categoryName, skip, Take);

            var mappedTrips = this.MapTrips(trips);

            var total = this.GetTripsTotal(categoryName);

            var categoryModel = new CategoryDetailsViewModel();
            categoryModel.Trips = mappedTrips;
            categoryModel.Total = total;
            categoryModel.Name = categoryName;
            categoryModel.Page = page;
            categoryModel.Take = Take;

            return this.View(categoryModel);
        }

        private List<TripViewModel> MapTrips(IEnumerable<ITrip> trips)
        {
            var mappedTrips = new List<TripViewModel>();
            foreach (var trip in trips)
            {
                var mapper = MapperProvider.Mapper;
                var mappedTrip = mapper.Map<TripViewModel>(trip);
                mappedTrips.Add(mappedTrip);
            }

            return mappedTrips;
        }

        private IEnumerable<ITrip> GetTrips(string categoryName, int skip, int take)
        {
            IEnumerable<ITrip> trips;
            if (categoryName == null)
            {
                trips = this.tripGetService.GetTrips(skip, take);
            }
            else
            {
                trips = this.tripGetService.GetTripsByCategoryName(categoryName, skip, take);
            }

            return trips;
        }

        private int GetTripsTotal(string categoryName)
        {
            int total;
            if (categoryName == null)
            {
                total = this.tripGetService.GetTripsCount();
            }
            else
            {
                total = this.tripGetService.GetTripsCountByCategoryName(categoryName);
            }

            return total;
        }
    }
}