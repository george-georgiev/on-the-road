using System;
using System.Web;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Views.Interfaces;
using WebFormsMvp;

namespace OnTheRoad.Presenters
{
    public class RegisterPresenter : Presenter<IRegisterView>
    {
        private IAuthenticationServiceFactory authenticationServiceFactory;

        public RegisterPresenter(IRegisterView view, IAuthenticationServiceFactory authServiceFactory)
            : base(view)
        {
            this.authenticationServiceFactory = authServiceFactory;
            View.CreateUser += CreateUser;
        }

        private void CreateUser(object sender, RegisterEventArgs e)
        {
            var authService = authenticationServiceFactory.GetRegisterService(this.HttpContext.GetOwinContext());
            
            try
            {
                authService.CreateUser(e.Username, e.UserEmail, e.UserPassword);
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