using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Domain.Repositories
{
    public interface ITripRepository : IRepository<ITrip>
    {
        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending);
    }
}
