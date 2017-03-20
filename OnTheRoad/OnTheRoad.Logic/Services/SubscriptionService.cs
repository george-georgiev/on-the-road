using System;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Factories;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using System.Linq;

namespace OnTheRoad.Logic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionAddHelper subscriptionAddHelper;
        private readonly ISubscriptionDataUtil subscriptionDataUtil;
        private readonly ISubscriptionFactory subscriptionFactory;

        public SubscriptionService(ISubscriptionDataUtil subscriptionDataUtil, ISubscriptionAddHelper subscriptionAddHelper, ISubscriptionFactory subscriptionFactory)
        {
            if (subscriptionDataUtil == null)
            {
                throw new ArgumentNullException("subscriptionDataUtil cannot be null!");
            }

            if (subscriptionAddHelper == null)
            {
                throw new ArgumentNullException("subscriptionAddHelper cannot be null!");
            }

            if (subscriptionFactory == null)
            {
                throw new ArgumentNullException("subscriptionFactory cannot be null!");
            }

            this.subscriptionDataUtil = subscriptionDataUtil;
            this.subscriptionAddHelper = subscriptionAddHelper;
            this.subscriptionFactory = subscriptionFactory;
        }

        public void AddOrUpdateSubscription(string username, int tripId, SubscriptionStatus status)
        {
            var subscription = this.subscriptionDataUtil.GetSubscription(username, tripId);
            if (subscription != null)
            {
                subscription.Status = status;
                this.subscriptionDataUtil.UpdateSubscription(subscription);
            }
            else
            {
                subscription =  this.subscriptionFactory.CreateSubscription(status);
                this.subscriptionAddHelper.SetSubscriptionUserByUsername(subscription, username);
                this.subscriptionAddHelper.SetSubscriptionTripById(subscription, tripId);
                this.subscriptionDataUtil.AddSubscription(subscription);
            }
        }

        public SubscriptionStatus GetUserSubscriptionStatus(ITrip trip, string userName)
        {
            var subscription = trip.Subscriptions
                .Where(s => s.User.Username == userName)
                .SingleOrDefault();

            var subscriptionStatus = subscription != null ? subscription.Status : SubscriptionStatus.None;

            return subscriptionStatus;
        }
    }
}
