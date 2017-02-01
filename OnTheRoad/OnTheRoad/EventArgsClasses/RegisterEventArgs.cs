using Microsoft.Owin;
using System;

namespace OnTheRoad.EventArgsClasses
{
    public class RegisterEventArgs : EventArgs
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public IOwinContext OwinContext{ get; set; }
    }
}