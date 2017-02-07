using System;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.Enums;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Account.Contracts;
using WebFormsMvp;

namespace OnTheRoad.Presenters
{
    public class LoginPresenter : Presenter<ILoginView>
    {
        private readonly IAuthenticationServiceFactory authenticationServiceFactory;

        public LoginPresenter(ILoginView view, IAuthenticationServiceFactory authServiceFactory)
            : base(view)
        {
            if (authServiceFactory == null)
            {
                throw new ArgumentNullException("Authentication Factory cannot be null");
            }

            this.authenticationServiceFactory = authServiceFactory;
            this.View.LoginUser += View_LogInUser;
        }

        private void View_LogInUser(object sender, LoginEventArgs e)
        {
            var loginService = authenticationServiceFactory.GetLoginService(e.OwinContext);

            var result = loginService.LoginUser(e.UserEmail, e.UserPassword, e.RememberMe);

            this.View.Model.LoginStatus = (LoginStatus)Enum.Parse(typeof(LoginStatus), result, true);
        }
    }
}