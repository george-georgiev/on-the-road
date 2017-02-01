using System;

namespace OnTheRoad.EventArgsClasses
{
    public class RegisterEventArgs : EventArgs
    {
        public string Username { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
    }
}