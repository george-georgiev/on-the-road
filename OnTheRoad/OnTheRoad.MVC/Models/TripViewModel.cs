using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;

namespace OnTheRoad.MVC.Models
{
    public class TripViewModel : IMapFrom<ITrip>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] CoverImage { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public string OrganiserUsername { get; set; }

        public string OrganiserFirstName { get; set; }

        public string OrganiserLastName { get; set; }

        public string Description { get; set; }
    }
}