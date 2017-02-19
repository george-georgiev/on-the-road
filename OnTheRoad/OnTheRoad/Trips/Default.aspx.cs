using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Domain.Models;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Trips
{
    [PresenterBinding(typeof(TripsPresenter))]
    public partial class Default : MvpPage<TripsModel>, ITripsView
    {
        protected const int PageSize = 3;
        private const string TripIdParam = "tripId";

        public event EventHandler<TripsEventArgs> GetTrip;
        public event EventHandler<TripsEventArgs> GetTrips;
        public event EventHandler<TripsEventArgs> GetTripsBySearchPattern;
        public event EventHandler<TripsEventArgs> Subscribe;

        public string TripId
        {
            get
            {
                var tripId = (string)this.RouteData.Values[TripIdParam];

                return tripId;
            }
        }

        public ITrip Trip { get; private set; }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (this.TripId != null)
            {
                var currentUserName = this.Context.User.Identity.Name;
                this.GetTrip(this, new TripsEventArgs() { TripId = int.Parse(this.TripId), CurrentUserName = currentUserName });
                this.Trip = this.Model.Trip;

                this.HandleAttendanceDropDown();

                this.PlaceHolderTrip.Visible = true;
                this.PlaceHolderTripsResult.Visible = false;
            }
        }

        protected void DropDownListAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = this.DropDownListAttendance.SelectedValue;
            var subscriptionStatus = (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), selectedValue);
            var currentUserName = this.Context.User.Identity.Name;
            this.Subscribe?.Invoke(this, new TripsEventArgs() { CurrentUserName = currentUserName, TripId = int.Parse(this.TripId), SubscriptionStatus = subscriptionStatus });
        }

        private void HandleAttendanceDropDown()
        {
            if (this.Model.IsOrganiser)
            {
                this.DropDownListAttendance.Visible = false;
            }
            else
            {
                var selectedValue = Enum.GetName(typeof(SubscriptionStatus), this.Model.SubscriptionStatus);
                this.DropDownListAttendance.SelectedValue = selectedValue;
                this.DropDownListAttendance.Visible = true;
            }
        }
    }
}