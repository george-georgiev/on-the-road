using System;
using WebFormsMvp;
using WebFormsMvp.Web;
using OnTheRoad.Common;
using System.Web;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Account.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Enums;

namespace OnTheRoad.Account
{
    [PresenterBinding(typeof(LoginPresenter))]
    public partial class Login : MvpPage<LoginModel>, ILoginView
    {
        public event EventHandler<LoginEventArgs> LoginUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                LoginUser?.Invoke(sender, new LoginEventArgs() {
                    Username = this.Username.Text,
                    UserPassword = this.Password.Text,
                    RememberMe = this.RememberMe.Checked,
                    OwinContext = this.Context.GetOwinContext()});
              
                switch (this.Model.LoginStatus)
                {
                    case LoginStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case LoginStatus.LockedOut:
                        this.Response.Redirect("/Account/Lockout");
                        break;
                    case LoginStatus.Failure:
                    default:
                        this.FailureText.Text = "Невалиден имейл или парола!";
                        this.ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}