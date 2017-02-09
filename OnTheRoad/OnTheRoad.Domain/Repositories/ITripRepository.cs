using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface ITripRepository : IGetRepository<ITrip>, IModifyRepository<ITrip>
    {
        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending);
    }
}
