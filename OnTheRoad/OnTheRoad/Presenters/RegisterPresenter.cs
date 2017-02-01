using System;
using System.Web;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Account.Interfaces;
using WebFormsMvp;
using Microsoft.Owin;

namespace OnTheRoad.Presenters
{
    public class RegisterPresenter : Presenter<IRegisterView>
    {
        private readonly IAuthenticationServiceFactory authenticationServiceFactory;

        public RegisterPresenter(IRegisterView view, IAuthenticationServiceFactory authServiceFactory)
            : base(view)
        {
            if (authServiceFactory == null)
            {
                throw new ArgumentNullException("Authentication Factory cannot be null");
            }

            this.authenticationServiceFactory = authServiceFactory;
            View.CreateUser += Create_User;
        }

        private void Create_User(object sender, RegisterEventArgs e)
        {
            var registerService = authenticationServiceFactory.GetRegisterService(e.OwinContext);
            
            try
            {
                registerService.CreateUser(e.UserEmail, e.UserPassword);
                View.Model.HasSucceeded = true;
            }
            catch (ArgumentException err)
            {
                View.Model.HasSucceeded = false;
                View.Model.ErrorMsg = err.Message;
            }
        }
    }
}