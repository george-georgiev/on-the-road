using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IReviewAddHelper
    {
        IRating GetRatingByValue(RatingEnum value);

        IUser GetUserByUsername(string username);
    }
}
