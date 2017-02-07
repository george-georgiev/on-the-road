using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        IUser GetByUserName(string userName);
    }
}
