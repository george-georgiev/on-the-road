using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IUserGetService
    {
        IUser GetUserInfo(string username);
    }
}
