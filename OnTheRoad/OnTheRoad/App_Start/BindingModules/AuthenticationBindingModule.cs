using System;
using System.Linq;
using Microsoft.Owin;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;

using OnTheRoad.App_Start.Factories;

using Microsoft.AspNet.Identity.Owin;

using OnTheRoad.Identity;
using OnTheRoad.Identity.Interfaces;
using OnTheRoad.Presenters;

namespace OnTheRoad.App_Start.BindingModules
{
    public class AuthenticationBindingModule : NinjectModule
    {
        public override void Load()
        {
            //this.Bind<IRegisterService>()
            //    .To<AuthenticationService>();

            this.Bind<IAuthenticationServiceFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IRegisterService>()
                .ToMethod(this.AuthenticationServiceFactoryMethod)
                .Named("RegisterService");

            this.Bind<ILoginService>()
                .ToMethod(this.AuthenticationServiceFactoryMethod)
                .Named("LoginService");

            this.Bind<IConfigureAuthServiceFactory>()
                .ToFactory()
                .InSingletonScope();

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