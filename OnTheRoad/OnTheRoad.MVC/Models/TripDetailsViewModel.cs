using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Models
{
    public class TripDetailsViewModel : IMapFrom<ITrip>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] CoverImage { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OrganiserUsername { get; set; }

        public string OrganiserFirstName { get; set; }

        public string OrganiserLastName { get; set; }

        public byte[] OrganiserImage { get; set; }

        public string Description { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public bool CanSubscribe { get; set; }

        public bool IsNone { get; set; }

        public bool IsAttending { get; set; }

        public bool IsInterested { get; set; }
    }
}