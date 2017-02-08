using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class City : ICity, IIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
