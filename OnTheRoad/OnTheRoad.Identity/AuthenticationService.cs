using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Linq;

namespace OnTheRoad.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(IOwinContext context)
        {
            this.Context = context;
        }

        private IOwinContext Context { get; set; }

        public void CreateUser(string username, string email, string password)
        {
            var manager = this.Context.GetUserManager<ApplicationUserManager>();
            var user = new ApplicationUser() { UserName = email, Email = email };
            IdentityResult result = manager.Create(user, password);

            if (result.Succeeded)
            {
                var signInManager = this.Context.Get<ApplicationSignInManager>();
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

            }
            else
            {
                // TODO: Create custom exception?
                throw new ArgumentException(result.Errors.FirstOrDefault());
            }
        }
    }
}
