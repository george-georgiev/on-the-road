using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Domain.Models
{
    public interface ISubscribtion : IIdentifiable
    {
        ITrip Trip { get; set; }

        SubscriptionStatus Status { get; set; }

        IUser User { get; set; }
    }
}
