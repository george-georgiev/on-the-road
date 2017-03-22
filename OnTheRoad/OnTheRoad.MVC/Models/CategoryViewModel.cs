using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;

namespace OnTheRoad.MVC.Models
{
    public class CategoryViewModel : IMapBothWays<ICategory>
    {
        public string Name { get; set; }
    }
}