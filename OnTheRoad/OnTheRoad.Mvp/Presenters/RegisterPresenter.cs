using OnTheRoad.Mvp.Account.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Factories;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
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
                registerService.CreateUser(e.Username, e.UserEmail, e.UserPassword, e.FirstName, e.LastName);
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