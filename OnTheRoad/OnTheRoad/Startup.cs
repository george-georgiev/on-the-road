using Microsoft.Owin;
using OnTheRoad.Identity;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnTheRoad.Startup))]
namespace OnTheRoad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
