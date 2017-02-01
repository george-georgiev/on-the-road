using System;
using System.Web;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Account.Interfaces;
using WebFormsMvp;

namespace OnTheRoad.Presenters
{
    public class RegisterPresenter : Presenter<IRegisterView>
    {
        private readonly IAuthenticationServiceFactory authenticationServiceFactory;

        public RegisterPresenter(IRegisterView view, IAuthenticationServiceFactory authServiceFactory)
            : base(view)
        {
            this.authenticationServiceFactory = authServiceFactory;
            View.CreateUser += CreateUser;
        }

        private void CreateUser(object sender, AuthEventArgs e)
        {
            var registerService = authenticationServiceFactory.GetRegisterService(this.HttpContext.GetOwinContext());
            
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