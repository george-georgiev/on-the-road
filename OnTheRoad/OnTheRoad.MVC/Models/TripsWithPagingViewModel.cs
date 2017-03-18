using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnTheRoad.MVC.Models
{
    public class TripsWithPagingViewModel
    {
        public string Heading { get; set; }

        public string PageHyperLink { get; set; }

        public IEnumerable<TripViewModel> Trips { get; set; }

        public int Page { get; set; }

        public int Take { get; set; }

        public int Total { get; set; }
    }
}