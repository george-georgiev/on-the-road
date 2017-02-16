using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Mvp.Models
{
    public class ProfileInfoModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
     
        public string Info { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public byte[] Image { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<IUser> FavouriteUsers { get; set; }
    }
}