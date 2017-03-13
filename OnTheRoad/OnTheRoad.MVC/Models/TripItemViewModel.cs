using System;

namespace OnTheRoad.MVC.Models
{
    public class TripItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] CoverImage { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public string OrganiserUserName { get; set; }

        public string OrganizerFirstName { get; set; }

        public string OrganizerLastName { get; set; }
    }
}