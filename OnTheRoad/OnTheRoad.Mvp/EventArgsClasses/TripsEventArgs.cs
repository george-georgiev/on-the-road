using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class TripsEventArgs
    {
        public string SearchPattern { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public int TripId { get; set; }
    }
}
