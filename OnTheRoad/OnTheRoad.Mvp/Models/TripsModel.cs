using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Mvp.Models
{
    public class TripsModel
    {
        public IEnumerable<ITrip> Trips { get; set; }

        public int TripsTotalCount { get; set; }

        public ITrip Trip { get; set; }

        public SubscriptionStatus SubscriptionStatus { get; set; }

        public bool IsOrganiser { get; set; }
    }
}