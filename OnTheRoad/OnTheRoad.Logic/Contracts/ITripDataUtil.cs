using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripDataUtil
    {
        ITrip GetTripById(int tripId);

        void AddTrip(ITrip trip);

        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take);

        int GetTripsCountByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsBySearchPattern(string pattern, int skip, int take);

        int GetTripsCountBySearchPattern(string pattern);

        IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending);
    }
}
