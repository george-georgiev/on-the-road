using OnTheRoad.App_Start.Factories;
using OnTheRoad.Enums;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Account.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFormsMvp;

namespace OnTheRoad.Presenters
{
    public class LoginPresenter : Presenter<ILoginView>
    {
        private readonly IAuthenticationServiceFactory authenticationServiceFactory;

        public LoginPresenter(ILoginView view, IAuthenticationServiceFactory authServiceFactory)
            : base(view)
        {
            this.authenticationServiceFactory = authServiceFactory;
            View.LoginUser += View_LogInUser;
        }

        private void View_LogInUser(object sender, LoginEventArgs e)
        {
            var loginService = authenticationServiceFactory.GetLoginService(e.OwinContext);

            var result = loginService.LoginUser(e.UserEmail, e.UserPassword, e.RememberMe);

            View.Model.LoginStatus = (LoginStatus)Enum.Parse(typeof(LoginStatus), result, true);
        }
    }
}