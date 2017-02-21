using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Contracts
{
    public interface ISubscriptionService
    {
        void AddOrUpdateSubscription(string username, int tripId, SubscriptionStatus status);
    }
}
