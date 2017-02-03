using Owin;
using OnTheRoad.App_Start;
using Ninject;
using OnTheRoad.Identity;

namespace OnTheRoad
{
    public partial class Startup
    {

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            var configureAuthService = NinjectKernelInstanceProvider.Instance.Get<ConfigureAuthService>();
            configureAuthService.Configure(app);
        }
    }
}
