using System;
using System.Web;
using WebFormsMvp;
using WebFormsMvp.Web;
using OnTheRoad.Common;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Account.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Account
{
    [PresenterBinding(typeof(RegisterPresenter))]
    public partial class Register : MvpPage<RegisterModel>, IRegisterView
    {
        public event EventHandler<RegisterEventArgs> CreateUser;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (CreateUser == null)
            {
                return;
            }

            CreateUser(this, new RegisterEventArgs
            {
                UserEmail = this.Email.Text,
                UserPassword = this.Password.Text,
                FirstName = this.FirstName.Text,
                LastName = this.LastName.Text,
                Username = this.Username.Text,
                OwinContext = this.Context.GetOwinContext()
            });

            if (this.Model.HasSucceeded)
            {
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                this.ErrorMessage.Text = this.Model.ErrorMsg;
            }
        }
    }
}