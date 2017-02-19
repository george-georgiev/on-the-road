using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Logic.Models
{
    public class Subscription : ISubscription
    {
        public Subscription(SubscriptionStatus status)
        {
            this.Status = status;
        }

        public int Id { get; set; }

        public SubscriptionStatus Status { get; set; }

        public ITrip Trip { get; set; }

        public IUser User { get; set; }
    }
}
