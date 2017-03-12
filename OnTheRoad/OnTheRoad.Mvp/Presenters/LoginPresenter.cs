using System;
using OnTheRoad.Mvp.Account.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Factories;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
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

            var result = loginService.LoginUser(e.Username, e.UserPassword, e.RememberMe);

            this.View.Model.LoginStatus = result;
        }
    }
}