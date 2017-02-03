using System;

using WebFormsMvp;
using WebFormsMvp.Web;

using OnTheRoad.Models;
using OnTheRoad.Presenters;
using OnTheRoad.Account.Interfaces;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Common;
using System.Web.UI.WebControls;
using System.Web;

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

            CreateUser(this, new RegisterEventArgs { UserEmail = this.Email.Text, UserPassword = this.Password.Text, OwinContext = this.Context.GetOwinContext() });

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