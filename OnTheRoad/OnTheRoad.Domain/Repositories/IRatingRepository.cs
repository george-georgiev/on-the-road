using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IRatingRepository
    {
        IRating GetByValue(string value);
    }
}
