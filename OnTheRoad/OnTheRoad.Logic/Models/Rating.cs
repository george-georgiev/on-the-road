using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class Rating : IRating
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
