using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IReviewRepository : IGetRepository<IReview>, IModifyRepository<IReview>
    {
        IEnumerable<IReview> GetByToUser(string toUser);

        IEnumerable<IReview> GetByToUser(string toUser, int skip, int take);

        int GetByToUserTotal(string toUser);
    }
}
