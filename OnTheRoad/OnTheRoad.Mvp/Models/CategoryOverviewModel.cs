using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Mvp.Models
{
    public class CategoryOverviewModel
    {
        public IEnumerable<ITrip> Trips { get; set; }
    }
}