using System;

using WebFormsMvp;

using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Views;

namespace OnTheRoad.Mvp.Presenters
{
    public class TripsListPresenter : Presenter<ITripsListView>
    {
        private readonly ITripGetService tripGetService;

        public TripsListPresenter(ITripsListView view, ITripGetService tripGetService) : base(view)
        {
            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService can not be null!");
            }

            this.tripGetService = tripGetService;
            this.View.GetTrips += View_GetTrips;
        }

        private void View_GetTrips(object sender, TripsListEventArgs e)
        {
            var categoryName = e.CategoryName;
            var trips = this.tripGetService.GetTripsByCategoryName(categoryName);
            this.View.Model.Trips = trips;
        }
    }
}