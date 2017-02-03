using Ninject;
using OnTheRoad.App_Start;
using OnTheRoad.App_Start.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WebFormsMvp.Binder;
using OnTheRoad.Identity;

namespace OnTheRoad
{
    public class Global : HttpApplication
    {
        [Inject]
        private IPresenterFactory PresenterFactory { get; set; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var customPresenterFactory = NinjectKernelInstanceProvider.Instance.Get<IPresenterFactory>();
            PresenterBinder.Factory = customPresenterFactory;

            var authServiceFactory = NinjectKernelInstanceProvider.Instance.Get<IConfigureAuthServiceFactory>();
            AuthConfiguration.Instance = authServiceFactory.CreateConfigureAuthService();
        }
    }
}