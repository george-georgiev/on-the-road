using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface ISubscriptionRepository : IGetRepository<ISubscription>, IModifyRepository<ISubscription>
    {
        ISubscription GetSubscription(string username, int tripId);
    }
}
