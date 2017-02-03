using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Domain.Models
{
    public interface ISubscribtion
    {
        ITrip Trip { get; }

        SubscriptionStatus Status { get; }

        IUser User { get; }
    }
}
