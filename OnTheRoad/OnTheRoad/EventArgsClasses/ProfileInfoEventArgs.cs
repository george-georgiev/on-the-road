using System.Collections.Generic;

namespace OnTheRoad.EventArgsClasses
{
    public class ProfileInfoEventArgs
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Info { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string ImagePath { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<string> FavouriteUsers { get; set; }
    }
}