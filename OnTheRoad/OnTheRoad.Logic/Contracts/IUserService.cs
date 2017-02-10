using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IUserService
    {
        IUser GetUserInfo(string id);

        void UpdateUserInfo(IUser user, string firstName, string lastName, string phoneNumber, string info, ICity city);
    }
}
