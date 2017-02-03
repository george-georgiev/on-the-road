using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface IUser
    {
        ICity City { get; }

        string Phone { get; }

        ICollection<IUser> FavouriteUsers { get; }

        IImage Image { get; }

        string PersonalInfo { get; }

        ISubscribtion Subscription { get; }

        ICollection<IReview> Reviews { get; }
    }
}
