using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripDataUtil
    {
        void AddTrip(ITrip trip);

        IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take);

        int GetTripsCountByCategoryName(string categoryName);

        IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending);
    }
}
