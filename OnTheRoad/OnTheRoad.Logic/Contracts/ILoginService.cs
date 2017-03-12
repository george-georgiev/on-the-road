using OnTheRoad.Logic.Enums;

namespace OnTheRoad.Logic.Contracts
{
    public interface ILoginService
    {
        LoginStatus LoginUser(string username, string password, bool rememberMe);
    }
}
