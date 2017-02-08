using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IUserRepository
    {
        IUser GetByUserName(string userName);

        IEnumerable<IUser> GetAll();

        IUser GetById(object id);

        void Delete(IUser entity);

        void Update(IUser entity);
    }
}
