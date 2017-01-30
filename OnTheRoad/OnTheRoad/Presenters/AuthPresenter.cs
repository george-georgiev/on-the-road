using System;
using OnTheRoad.Identity;
using OnTheRoad.Views.EventArgsClasses;
using OnTheRoad.Views.Interfaces;
using WebFormsMvp;
using System.Web;
using OnTheRoad.App_Start.Factories;

namespace OnTheRoad.Presenters
{
    public class AuthPresenter : Presenter<IAuthView>
    {
        private IAuthenticationServiceFactory authenticationServiceFactory;

        public AuthPresenter(IAuthView view, IAuthenticationServiceFactory authServiceFactory)
            : base(view)
        {
            this.authenticationServiceFactory = authServiceFactory;
            View.Load += ViewLoad;
            View.CreateUser += CreateUser;
        }

        void ViewLoad(object sender, EventArgs e)
        {

        }

        void CreateUser(object sender, AuthEventArgs e)
        {
            var authService = authenticationServiceFactory.GetAuthenticationService(this.HttpContext.GetOwinContext());

            try
            {
                authService.CreateUser(e.Username, e.UserEmail, e.UserPassword);
                View.Model.HasSucceeded = true;
                View.Model.Username = e.Username;
                View.Model.UserEmail = e.UserEmail;
            }
            catch (ArgumentException err)
            {
                View.Model.HasSucceeded = false;
                View.Model.ErrorMsg = err.Message;
            }
        }
    }
}