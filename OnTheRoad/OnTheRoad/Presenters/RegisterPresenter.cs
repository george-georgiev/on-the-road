using System;
using System.Web;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.EventArgsClasses;
using OnTheRoad.Account.Contracts;
using WebFormsMvp;
using Microsoft.Owin;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Presenters
{
    public class RegisterPresenter : Presenter<IRegisterView>
    {
        private readonly IAuthenticationServiceFactory authenticationServiceFactory;
        //private readonly ICategoryService categoryService;

        public RegisterPresenter(IRegisterView view, IAuthenticationServiceFactory authServiceFactory/*, ICategoryService categoryService*/)
            : base(view)
        {
            if (authServiceFactory == null)
            {
                throw new ArgumentNullException("Authentication Factory cannot be null");
            }

            //this.categoryService = categoryService;

            this.authenticationServiceFactory = authServiceFactory;
            View.CreateUser += Create_User;
        }

        private void Create_User(object sender, RegisterEventArgs e)
        {
            //this.categoryService.AddCategory("name");
            //var category = this.categoryService.GetCategoryByName("name");
            var registerService = authenticationServiceFactory.GetRegisterService(e.OwinContext);
            
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