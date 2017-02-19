using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface ISubscriptionDataUtil
    {
        ISubscription GetSubscription(string username, int tripId);

        void AddSubscription(ISubscription subscription);

        void UpdateSubscription(ISubscription subscription);
    }
}
