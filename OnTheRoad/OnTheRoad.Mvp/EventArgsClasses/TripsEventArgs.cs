using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class TripsEventArgs
    {
        public string SearchPattern { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public int TripId { get; set; }

        public string CurrentUserName { get; set; }

        public SubscriptionStatus SubscriptionStatus { get; set; }
    }
}
