using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class GetTripEventArgs : EventArgs
    {
        public string CurrentUserName { get; set; }

        public int TripId { get; set; }
    }
}
