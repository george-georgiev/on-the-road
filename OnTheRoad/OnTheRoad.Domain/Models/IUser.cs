﻿using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface IUser 
    {
        string Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Info { get; set; }

        ICountry Country { get; set; }

        ICity City { get; set; }

        string PhoneNumber { get; set; }

        ICollection<IUser> FavouriteUsers { get; set; }

        IImage Image { get; set; }

        string PersonalInfo { get; set; }

        ICollection<ISubscribtion> Subscription { get; set; }

        ICollection<IReview> Reviews { get; set; }
    }
}
