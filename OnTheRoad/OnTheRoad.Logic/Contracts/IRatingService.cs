using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface IRatingService
    {
        IRating GetRatingByValue(string value);
    }
}
