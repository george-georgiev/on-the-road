using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class TripsPresenter : Presenter<ITripsView>
    {
        private readonly ITripGetService tripGetService;

        public TripsPresenter(ITripsView view, ITripGetService tripGetService) : base(view)
        {
            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService can not be null!");
            }

            this.tripGetService = tripGetService;

            this.View.GetTripById += View_GetTripById;
        }

        private void View_GetTripById(object sender, EventArgsClasses.TripsEventArgs e)
        {
            var tripId = e.TripId;
            var trip = this.tripGetService.GetTripById(tripId);
            this.View.Model.Trip = trip;
        }
    }
}
