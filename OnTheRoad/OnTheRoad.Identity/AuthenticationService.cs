using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Data.Models;
using System.IO;

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

        public void CreateUser(string username, string email, string password, string firstName, string lastName)
        {
            var user = new User() { UserName = username, Email = email, Info = null, FirstName = firstName, LastName = lastName };
            IdentityResult result = this.AppUserManager.Create(user, password);

            if (result.Succeeded)
            {
                var currentUser = this.AppUserManager.FindByName(username);
                this.AppUserManager.AddToRole(currentUser.Id, "User");

                this.AppSignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            else
            {
                throw new ArgumentException(result.Errors.FirstOrDefault());
            }
        }

        public string LoginUser(string username, string password, bool rememberMe)
        {
            SignInStatus result = this.AppSignInManager.PasswordSignIn(username, password, rememberMe, shouldLockout: true);
            return result.ToString();
        }
    }
}
