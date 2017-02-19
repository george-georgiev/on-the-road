using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface ISubscriptionAddHelper
    {
        void SetSubscriptionUserByUsername(ISubscription subscription, string username);

        void SetSubscriptionTripById(ISubscription subscription, int tripId);
    }
}
