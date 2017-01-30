using System;

namespace OnTheRoad.Views.EventArgsClasses
{
    public class AuthEventArgs : EventArgs
    {
        public string Username { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
    }
}