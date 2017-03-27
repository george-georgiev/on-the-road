using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface ITripRepository : IGetRepository<ITrip>, IModifyRepository<ITrip>
    {
        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take);

        int GetTripsCountByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsBySearchPattern(string pattern, int skip, int take);

        int GetTripsCountBySearchPattern(string pattern);

        IEnumerable<ITrip> GetTrips(int skip, int take);

        int GetTripsCount();

        IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending);

        IEnumerable<ITrip> GetUserAttendingTrips(string username, int skip, int take);

        int GetUserAttendingTripsCount(string username);
    }
}
