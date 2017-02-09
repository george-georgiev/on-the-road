using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IUserService
    {
        IUser GetUserInfo(string id);

        void UpdateUserInfo(IUser user, string firstName, string lastName, string email, string phoneNumber, string info, ICity city);

        bool ChechIfUsernameExists(string username);
    }
}
