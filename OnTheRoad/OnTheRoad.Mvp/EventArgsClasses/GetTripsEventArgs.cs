using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class GetTripsEventArgs
    {
        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
