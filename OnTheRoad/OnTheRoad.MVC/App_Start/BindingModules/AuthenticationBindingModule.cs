using System;
using System.Linq;
using Microsoft.Owin;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;

using Microsoft.AspNet.Identity.Owin;

using OnTheRoad.Identity;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Factories;

namespace OnTheRoad.MVC.App_Start.BindingModules
{
    public class AuthenticationBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthenticationServiceFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRegisterService>()
                .ToMethod(this.AuthenticationServiceFactoryMethod)
                .Named("RegisterService");

            this.Bind<ILoginService>()
                .ToMethod(this.AuthenticationServiceFactoryMethod)
                .Named("LoginService");

            this.Bind<ConfigureAuthService>().ToSelf();
        }

        private AuthenticationService AuthenticationServiceFactoryMethod(IContext ctx)
        {
            var parameters = ctx.Parameters.ToList();

            var context = parameters[0].GetValue(ctx, null) as IOwinContext;
            if (context == null)
            {
                throw new ArgumentNullException("Invalid requested context type.");
            }

            var appUserManager = context.GetUserManager<ApplicationUserManager>();
            var appSignInManager = context.Get<ApplicationSignInManager>();

            return new AuthenticationService(appUserManager, appSignInManager);
        }
    }
}