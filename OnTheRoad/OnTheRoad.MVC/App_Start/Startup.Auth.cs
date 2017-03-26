using Owin;
using OnTheRoad.MVC.App_Start;
using OnTheRoad.Identity;
using Ninject;
using Microsoft.AspNet.SignalR;

namespace OnTheRoad.MVC
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var configureAuthService = NinjectKernelInstanceProvider.Instance.Get<ConfigureAuthService>();
            configureAuthService.Configure(app);
        }

        public void ConfigureSignalR(IAppBuilder app)
        {
            var resolver = new NinjectSignalRDependencyResolver();
            var config = new HubConfiguration();
            config.Resolver = resolver;

            app.MapSignalR(config);
        }
    }
}