using System;
using WebFormsMvp;
using WebFormsMvp.Web;
using OnTheRoad.Account.Interfaces;
using OnTheRoad.Common;
using OnTheRoad.Enums;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Models;
using OnTheRoad.Presenters;

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
                LoginUser?.Invoke(sender, new LoginEventArgs() { UserEmail = this.Email.Text, UserPassword = this.Password.Text, RememberMe = this.RememberMe.Checked });

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
                        this.FailureText.Text = "Invalid login attempt";
                        this.ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}