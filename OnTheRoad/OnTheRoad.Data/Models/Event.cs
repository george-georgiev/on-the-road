using OnTheRoad.Domain.Models;

namespace OnTheRoad.Data.Models
{
    public class Event : IEvent
    {
        public Event()
        {

        }

        public Event(string name, string destination)
        {
            this.Name = name;
            this.Destination = destination;
        }

        public int Id { get; set; }

        public string Destination { get; set; }

        public string Name { get; set; }
    }
}
