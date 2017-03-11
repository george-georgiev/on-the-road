using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnTheRoad.MVC.Startup))]
namespace OnTheRoad.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
