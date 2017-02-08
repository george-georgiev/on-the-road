using Microsoft.Owin;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Mvp.Factories
{
    public interface IAuthenticationServiceFactory
    {
        IRegisterService GetRegisterService(IOwinContext owinContext);

        ILoginService GetLoginService(IOwinContext owinContext);
    }
}
