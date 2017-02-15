using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IGetUserService
    {
        IUser GetUserInfo(string username);
    }
}
