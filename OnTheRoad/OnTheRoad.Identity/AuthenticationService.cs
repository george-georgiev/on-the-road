using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Data.Models;

namespace OnTheRoad.Identity
{
    public class AuthenticationService : IRegisterService, ILoginService
    {
        public AuthenticationService(ApplicationUserManager appUserManager, ApplicationSignInManager appSignInManager)
        {
            this.AppUserManager = appUserManager;
            this.AppSignInManager = appSignInManager;
        }

        private ApplicationUserManager AppUserManager { get; set; }

        private ApplicationSignInManager AppSignInManager { get; set; }

        public void CreateUser(string email, string password, string firstName, string lastName)
        {
            var user = new User() { UserName = email, Email = email, Info = string.Empty, FirstName = firstName, LastName = lastName };
            IdentityResult result = this.AppUserManager.Create(user, password);

            if (result.Succeeded)
            {
                // Add initial User Role
                // var currentUser = this.AppUserManager.FindByName(email);
                // this.AppUserManager.AddToRole(currentUser.Id, "Admin");
                this.AppSignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            else
            {
                // TODO: Create custom exception?
                throw new ArgumentException(result.Errors.FirstOrDefault());
            }
        }

        public string LoginUser(string email, string password, bool rememberMe)
        {
            SignInStatus result = this.AppSignInManager.PasswordSignIn(email, password, rememberMe, shouldLockout: true);
            return result.ToString();
        }
    }
}
