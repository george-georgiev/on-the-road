using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class CityViewModel : IMapFrom<ICity>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}