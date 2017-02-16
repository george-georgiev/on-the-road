using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class GetUserReviewsEventArgs : EventArgs
    {
        public string Username { get; set; }
    }
}
