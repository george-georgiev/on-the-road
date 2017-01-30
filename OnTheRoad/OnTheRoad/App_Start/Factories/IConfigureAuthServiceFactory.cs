using OnTheRoad.Identity;

namespace OnTheRoad.App_Start.Factories
{
    public interface IConfigureAuthServiceFactory
    {
        ConfigureAuthService CreateConfigureAuthService();
    }
}
