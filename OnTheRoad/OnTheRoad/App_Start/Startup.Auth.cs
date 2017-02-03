using Owin;
using OnTheRoad.App_Start;

namespace OnTheRoad
{
    public partial class Startup
    {

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            AuthConfiguration.Instance.Configure(app);
        }
    }
}
