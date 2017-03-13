using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Models
{
    public class CategoryOverviewViewModel : IMapFrom<ICategory>
    {
        public string Name { get; set; }

        public IEnumerable<TripViewModel> Trips { get; set; }
    }
}