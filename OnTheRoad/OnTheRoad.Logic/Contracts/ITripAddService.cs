using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripAddService
    {
        void AddTrip(ITrip trip, string organizerUsername, IEnumerable<int> categoryIds, IEnumerable<string> tagNames);
    }
}