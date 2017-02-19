using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Contracts
{
    public interface ISubscriptionAddService
    {
        void AddOrUpdateSubscription(string username, int tripId, SubscriptionStatus status);
    }
}
