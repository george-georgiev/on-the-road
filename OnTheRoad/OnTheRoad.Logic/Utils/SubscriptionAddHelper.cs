using OnTheRoad.Logic.Contracts;
using System;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Utils
{
    public class SubscriptionAddHelper : ISubscriptionAddHelper
    {
        private readonly ITripGetService tripGetService;
        private readonly IUserGetService userGetService;

        public SubscriptionAddHelper(IUserGetService userGetService, ITripGetService tripGetService)
        {
            if (userGetService == null)
            {
                throw new ArgumentNullException("userGetService can not be null!");
            }

            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService can not be null!");
            }

            this.userGetService = userGetService;
            this.tripGetService = tripGetService;
        }

        public void SetSubscriptionTripById(ISubscription subscription, int tripId)
        {
            var trip = this.tripGetService.GetTripById(tripId);

            subscription.Trip = trip;
        }

        public void SetSubscriptionUserByUsername(ISubscription subscription, string username)
        {
            var user = this.userGetService.GetUserInfo(username);

            subscription.User = user;
        }
    }
}
