using Microsoft.Owin;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.App_Start.Factories
{
    public interface IAuthenticationServiceFactory
    {
        IRegisterService GetRegisterService(IOwinContext owinContext);

        ILoginService GetLoginService(IOwinContext owinContext);
    }
}
