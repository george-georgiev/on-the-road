using Microsoft.Owin;
using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class RegisterEventArgs : EventArgs
    {
        //TODO: Test First and LastName and Username
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public IOwinContext OwinContext{ get; set; }
    }
}