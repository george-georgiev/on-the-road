using OnTheRoad.Logic.Contracts;
using System;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Logic.Factories;

namespace OnTheRoad.Logic.Services
{
    public class SubscriptionAddService : ISubscriptionAddService
    {
        private readonly ISubscriptionAddHelper subscriptionAddHelper;
        private readonly ISubscriptionDataUtil subscriptionDataUtil;
        private readonly ISubscriptionFactory subscriptionFactory;

        public SubscriptionAddService(ISubscriptionDataUtil subscriptionDataUtil, ISubscriptionAddHelper subscriptionAddHelper, ISubscriptionFactory subscriptionFactory)
        {
            if (subscriptionDataUtil == null)
            {
                throw new ArgumentNullException("subscriptionDataUtil can not be null!");
            }

            if (subscriptionAddHelper == null)
            {
                throw new ArgumentNullException("subscriptionAddHelper can not be null!");
            }

            if (subscriptionFactory == null)
            {
                throw new ArgumentNullException("subscriptionFactory can not be null!");
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
    }
}
