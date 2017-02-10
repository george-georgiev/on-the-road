using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IUserService
    {
        IUser GetUserInfo(string id);

        void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber, string info, ICity city);

        void RemoveFavouriteUser(string username, string userToRemoveUsername);

        void AddFafouriteUser(string username, string userToAddUsername);
    }
}
