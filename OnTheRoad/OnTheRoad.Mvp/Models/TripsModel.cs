using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Mvp.Models
{
    public class TripsModel
    {
        public IEnumerable<ITrip> Trips { get; set; }
    }
}