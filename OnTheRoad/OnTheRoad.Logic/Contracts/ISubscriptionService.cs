using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface ISubscriptionService
    {
        void AddOrUpdateSubscription(string username, int tripId, SubscriptionStatus status);

        SubscriptionStatus GetUserSubscriptionStatus(ITrip trip, string userName);
    }
}
