using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.MVC.Areas.Administration.Models
{
    public class TripViewModel : IMapBothWays<ITrip>
    {
        [Editable(false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }
    }
}