using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class CategoriesPresenter : Presenter<ICategoriesView>
    {
        private readonly ICategoryService categoryService;
        private readonly ITripGetService tripGetService;

        public CategoriesPresenter(ICategoriesView view, ICategoryService categoryService, ITripGetService tripGetService) : base(view)
        {
            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService can not be null!");
            }

            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService can not be null!");
            }

            this.categoryService = categoryService;
            this.tripGetService = tripGetService;

            this.View.GetCategories += View_GetCategories;
            this.View.GetTrips += View_GetTrips;
            this.View.GetTripsTotalCount += View_GetTripsTotalCount;
        }

        private void View_GetTripsTotalCount(object sender, EventArgsClasses.CategoriesEventArgs e)
        {
            var categoryName = e.CategoryName;
            var totalCount = this.tripGetService.GetTripsCountByCategoryName(categoryName);
            this.View.Model.TripsTotalCount = totalCount;
        }

        private void View_GetTrips(object sender, EventArgsClasses.CategoriesEventArgs e)
        {
            var categoryName = e.CategoryName;
            var skip = e.Skip;
            var take = e.Take;
            var trips = this.tripGetService.GetTripsByCategoryName(categoryName, skip, take);
            this.View.Model.Trips = trips;
        }

        private void View_GetCategories(object sender, EventArgs e)
        {
            var categories = this.categoryService.GetAllCategories();
            this.View.Model.Categories = categories;
        }
    }
}
