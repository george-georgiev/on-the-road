using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Domain.Models;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;

namespace OnTheRoad.Trips
{
    [PresenterBinding(typeof(TripsPresenter))]
    public partial class Default : MvpPage<TripsModel>, ITripsView
    {
        protected const int PageSize = 3;
        private const string TripIdParam = "tripId";

        public event EventHandler<TripsEventArgs> GetTripById;
        public event EventHandler<TripsEventArgs> GetTrips;
        public event EventHandler<TripsEventArgs> GetTripsBySearchPattern;

        public string TripId
        {
            get
            {
                var tripId = (string)this.RouteData.Values[TripIdParam];

                return tripId;
            }
        }

        public ITrip Trip { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.TripId != null)
            {
                this.GetTripById(this, new TripsEventArgs() { TripId = int.Parse(this.TripId) });
                this.Trip = this.Model.Trip;

                this.PlaceHolderTripsResult.Visible = false;
                this.PlaceHolderTrip.Visible = true;
            }
        }

        protected void DropDownListAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}