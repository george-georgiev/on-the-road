using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class GetHomePageInfoEventArgs : EventArgs
    {
        public IEnumerable<ITrip> GetRecentTrips { get; set; }

        public int GetUsersCount { get; set; }

        public int GetTripsCount { get; set; }
    }
}
