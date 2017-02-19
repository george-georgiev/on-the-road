using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
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
        private readonly ISubscriptionAddService subscriptionAddService;

        public TripsPresenter(ITripsView view, ITripGetService tripGetService, ISubscriptionAddService subscriptionAddService) : base(view)
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
        }

        private void View_Subscribe(object sender, TripsEventArgs e)
        {
            var currentUserName = e.CurrentUserName;
            var tripId = e.TripId;
            var subscriptionStatus = e.SubscriptionStatus;
            this.subscriptionAddService.AddOrUpdateSubscription(currentUserName, tripId, subscriptionStatus);
        }

        private void View_GetTripById(object sender, TripsEventArgs e)
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
