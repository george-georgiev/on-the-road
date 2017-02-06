using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface IUser : IIdentifiable
    {
        ICity City { get; set; }

        string Phone { get; set; }

        ICollection<IUser> FavouriteUsers { get; set; }

        IImage Image { get; set; }

        string PersonalInfo { get; set; }

        ISubscribtion Subscription { get; set; }

        ICollection<IReview> Reviews { get; set; }
    }
}
