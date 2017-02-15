using Microsoft.Owin;
using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class LoginEventArgs : EventArgs
    {
        public string Username { get; set; }

        public string UserPassword { get; set; }

        public bool RememberMe { get; set; }

        public IOwinContext OwinContext { get; set; }
    }
}