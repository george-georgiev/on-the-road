using System;
using OnTheRoad.Identity;
using OnTheRoad.Views.EventArgsClasses;
using OnTheRoad.Views.Interfaces;
using WebFormsMvp;
using System.Web;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Presenters
{
    public class AuthPresenter : Presenter<IAuthView>
    {
        private IAuthenticationServiceFactory authenticationServiceFactory;
        private readonly IEventService eventService;

        public AuthPresenter(IAuthView view, IAuthenticationServiceFactory authServiceFactory, IEventService eventService)
            : base(view)
        {
            this.authenticationServiceFactory = authServiceFactory;
            View.Load += ViewLoad;
            View.CreateUser += CreateUser;
            this.eventService = eventService;
        }

        void ViewLoad(object sender, EventArgs e)
        {

        }

        void CreateUser(object sender, AuthEventArgs e)
        {
            var authService = authenticationServiceFactory.GetAuthenticationService(this.HttpContext.GetOwinContext());
            this.eventService.GetEvent();
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