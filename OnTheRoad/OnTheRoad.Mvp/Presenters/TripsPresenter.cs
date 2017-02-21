using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Views;
using System;
using System.Linq;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class TripsPresenter : Presenter<ITripsView>
    {
        private readonly ITripGetService tripGetService;
        private readonly ISubscriptionService subscriptionAddService;

        public TripsPresenter(ITripsView view, ITripGetService tripGetService, ISubscriptionService subscriptionAddService) : base(view)
        {
            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService can not be null!");
            }

            if (subscriptionAddService == null)
            {
                throw new ArgumentNullException("subscriptionAddService can not be null!");
            }

            this.tripGetService = tripGetService;
            this.subscriptionAddService = subscriptionAddService;

            this.View.GetTrip += View_GetTripById;
            this.View.Subscribe += View_Subscribe;
            this.View.GetTripsBySearchPattern += View_GetTripsBySearchPattern;
            this.View.GetTripsSearchTotalCount += View_GetTripsSearchTotalCount;
            this.View.GetTrips += View_GetTrips;
            this.View.GetTripsTotalCount += View_GetTripsTotalCount;
        }

        private void View_GetTripsTotalCount(object sender, GetTripsEventArgs e)
        {
            var count = this.tripGetService.GetTripsCount();
            this.View.Model.TripsTotalCount = count;
        }

        private void View_GetTrips(object sender, GetTripsEventArgs e)
        {
            var skip = e.Skip;
            var take = e.Take;
            var trips = this.tripGetService.GetTrips(skip, take);
            this.View.Model.Trips = trips;
        }

        private void View_GetTripsBySearchPattern(object sender, SearchTripsEventArgs e)
        {
            var pattern = e.SearchPattern;
            var skip = e.Skip;
            var take = e.Take;
            var trips = this.tripGetService.GetTripsBySearchPattern(pattern, skip, take);
            this.View.Model.Trips = trips;
        }

        private void View_GetTripsSearchTotalCount(object sender, SearchTripsEventArgs e)
        {
            var pattern = e.SearchPattern;
            var count = this.tripGetService.GetTripsCountBySearchPattern(pattern);
            this.View.Model.TripsTotalCount = count;
        }

        private void View_Subscribe(object sender, SubscribeEventArgs e)
        {
            var currentUserName = e.CurrentUserName;
            var tripId = e.TripId;
            var subscriptionStatus = e.SubscriptionStatus;
            this.subscriptionAddService.AddOrUpdateSubscription(currentUserName, tripId, subscriptionStatus);
        }

        private void View_GetTripById(object sender, GetTripEventArgs e)
        {
            var tripId = e.TripId;
            var trip = this.tripGetService.GetTripById(tripId);
            this.View.Model.Trip = trip;

            var currentUsername = e.CurrentUserName;
            var subscription = trip.Subscriptions
                .Where(s => s.User.Username == currentUsername)
                .SingleOrDefault();
            var subscriptionStatus = subscription != null ? subscription.Status : SubscriptionStatus.None;
            this.View.Model.SubscriptionStatus = subscriptionStatus;

            var isOrganiser = trip.Organiser.Username == currentUsername;
            this.View.Model.IsOrganiser = isOrganiser;
        }
    }
}
