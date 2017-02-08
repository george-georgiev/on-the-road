using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class CategoryOverviewPresenter : Presenter<ICategoryOverviewView>
    {
        private const int TripsCount = 4;
        private readonly ITripService tripService;

        public CategoryOverviewPresenter(ICategoryOverviewView view, ITripService tripService) : base(view)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService can not be null!");
            }

            this.tripService = tripService;

            this.View.OnPageLoad += View_OnPageLoad; ;
        }

        private void View_OnPageLoad(object sender, CategoryOverviewEventArgs e)
        {
            var trips = this.tripService.GetTripsOrderedByDateCreated(TripsCount);
            this.View.Model.Trips = trips;
        }
    }
}