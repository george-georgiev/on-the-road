using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using OnTheRoad.App_Start;
using OnTheRoad.Common;
using WebFormsMvp.Binder;

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

            CacheWrapper.Instance = this.Context.Cache;
        }
    }
}