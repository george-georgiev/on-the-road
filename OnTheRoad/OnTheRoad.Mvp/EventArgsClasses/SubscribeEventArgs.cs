using OnTheRoad.Domain.Enumerations;
using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class SubscribeEventArgs : GetTripEventArgs
    {
        public SubscriptionStatus SubscriptionStatus { get; set; }
    }
}
