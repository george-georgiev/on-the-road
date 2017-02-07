using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class User : IUser, IIdentifiable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
         
        public string Info { get; set; }

        public ICity City { get; set; }
         
        public ICountry Country { get; set; }

        public int Id { get; set; }

        public IImage Image { get; set; }
    
        public string PersonalInfo { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<IReview> Reviews { get; set; }

        public ICollection<ISubscribtion> Subscription { get; set; }

        public ICollection<IUser> FavouriteUsers { get; set; }
    }
}
