using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Factories
{
    public interface ISubscriptionFactory
    {
        ISubscription CreateSubscription(SubscriptionStatus status);
    }
}
