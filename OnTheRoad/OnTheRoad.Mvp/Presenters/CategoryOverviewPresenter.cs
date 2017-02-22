using System;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class CategoryOverviewPresenter : Presenter<ICategoryOverviewView>
    {
        private const int TripsCount = 4;
        private readonly ITripGetService tripService;

        public CategoryOverviewPresenter(ICategoryOverviewView view, ITripGetService tripService) : base(view)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService can not be null!");
            }

            this.tripService = tripService;

            this.View.GetTrips += this.View_GetTrips; ;
        }

        private void View_GetTrips(object sender, CategoryOverviewEventArgs e)
        {
            var categoryName = e.CategoryName;
            var trips = this.tripService.GetTripsByCategoryNameOrderedByDate(categoryName, TripsCount);
            this.View.Model.Trips = trips;
        }
    }
}