using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IReviewRepository : IGetRepository<IReview>, IModifyRepository<IReview>
    {
        IEnumerable<IReview> GetByToUser(IUser toUser);
    }
}
