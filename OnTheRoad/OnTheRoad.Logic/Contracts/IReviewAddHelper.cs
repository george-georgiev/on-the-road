using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IReviewAddHelper
    {
        IRating GetRatingByValue(string value);

        IUser GetUserByUsername(string username);
    }
}
