using Owin;
using OnTheRoad.MVC.App_Start;
using OnTheRoad.Identity;
using Ninject;

namespace OnTheRoad.MVC
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var configureAuthService = NinjectKernelInstanceProvider.Instance.Get<ConfigureAuthService>();
            configureAuthService.Configure(app);
        }
    }
}