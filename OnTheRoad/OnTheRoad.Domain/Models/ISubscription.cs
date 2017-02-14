using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Domain.Models
{
    public interface ISubscription : IIdentifiable
    {
        ITrip Trip { get; set; }

        SubscriptionStatus Status { get; set; }

        IUser User { get; set; }
    }
}
