using OnTheRoad.Logic.Contracts;
using System;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Domain.Contracts;

namespace OnTheRoad.Logic.Utils
{
    public class SubscriptionDataUtil : ISubscriptionDataUtil
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUnitOfWork unitOfWork;

        public SubscriptionDataUtil(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            if (subscriptionRepository == null)
            {
                throw new ArgumentNullException("subscriptionRepository can not be null!");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork can not be null!");
            }

            this.subscriptionRepository = subscriptionRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddSubscription(ISubscription subscription)
        {
            this.subscriptionRepository.Add(subscription);
            this.unitOfWork.Commit();
        }

        public ISubscription GetSubscription(string username, int tripId)
        {
            var subscription = this.subscriptionRepository.GetSubscription(username, tripId);

            return subscription;
        }

        public void UpdateSubscription(ISubscription subscription)
        {
            this.subscriptionRepository.Update(subscription);
            this.unitOfWork.Commit();
        }
    }
}
