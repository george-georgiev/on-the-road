using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Mvp.Models
{
    public class HomeModel
    {
        public IEnumerable<ITrip> RecentTrips { get; set; }

        public int AllTripsCount { get; set; }

        public int AllUsersCount { get; set; }
    }
}
