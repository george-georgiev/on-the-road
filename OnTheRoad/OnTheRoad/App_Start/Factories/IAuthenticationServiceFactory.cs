using Microsoft.Owin;
using OnTheRoad.Identity;

namespace OnTheRoad.App_Start.Factories
{
    public interface IAuthenticationServiceFactory
    {
        IAuthenticationService GetAuthenticationService(IOwinContext owinContext);
    }
}
