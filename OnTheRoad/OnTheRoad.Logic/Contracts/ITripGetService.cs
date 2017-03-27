using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripGetService
    {
        ITrip GetTripById(int tripId);

        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take);

        int GetTripsCountByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsBySearchPattern(string pattern, int skip, int take);

        int GetTripsCountBySearchPattern(string pattern);

        IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending = false);

        IEnumerable<ITrip> GetTrips(int skip, int take);

        int GetTripsCount();

        IEnumerable<ITrip> GetUserAttendingTrips(string username, int skip, int take);

        int GetUserAttendingTripsCount(string username);
    }
}
