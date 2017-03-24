using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class UserViewModel : IMapFrom<IUser>
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CityName { get; set; }

        public int CityId { get; set; }

        public string PhoneNumber { get; set; }

        [UIHint("Textarea")]
        public string Info { get; set; }

        public byte[] Image { get; set; }

        public IEnumerable<UserViewModel> FavouriteUsers { get; set; }
    }
}