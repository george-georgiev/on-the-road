using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IMessageRepository : IGetRepository<IMessage>, IModifyRepository<IMessage>
    {
    }
}
