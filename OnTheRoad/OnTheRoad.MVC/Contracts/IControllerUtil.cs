using OnTheRoad.Infrastructure.Enums;

namespace OnTheRoad.MVC.Contracts
{
    public interface IControllerUtil
    {
        string LoggedUserName { get; }

        bool IsAuthenticated { get; }

        void SetResponseStatusCode(ResponseStatus status);
    }
}
