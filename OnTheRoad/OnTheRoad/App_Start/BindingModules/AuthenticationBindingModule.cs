using System;
using System.Linq;
using Microsoft.Owin;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using OnTheRoad.App_Start.Factories;
using OnTheRoad.Identity;

namespace OnTheRoad.App_Start.BindingModules
{
    public class AuthenticationBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthenticationService>()
                .To<AuthenticationService>();

            this.Bind<IAuthenticationServiceFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IAuthenticationService>()
                .ToMethod(this.AuthenticationServiceFactoryMethod)
                .Named("AuthenticationService");

            this.Bind<IConfigureAuthServiceFactory>()
                .ToFactory()
                .InSingletonScope();

        }

        private IAuthenticationService AuthenticationServiceFactoryMethod(IContext ctx)
        {
            var parameters = ctx.Parameters.ToList();

            var context = parameters[0].GetValue(ctx, null) as IOwinContext;
            if (context == null)
            {
                throw new ArgumentNullException("Invalid requested context type.");
            }

            return new AuthenticationService(context);
        }
    }
}