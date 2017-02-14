using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripGetService
    {
        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending = false);
    }
}
