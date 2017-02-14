using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripAddHelper
    {
        void SetTripCategoriesById(ITrip trip, IEnumerable<int> idCollection);

        void SetTripTagsByName(ITrip trip, IEnumerable<string> tagNames);

        void SetTripOrganiserByUsername(ITrip trip, string username);
    }
}
