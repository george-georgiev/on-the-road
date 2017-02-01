using System;

namespace OnTheRoad.EventArgsClasses
{
    public class AuthEventArgs : EventArgs
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
    }
}