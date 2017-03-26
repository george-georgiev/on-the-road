using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface IUser
    {
        string Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Username { get; set; }

        string Info { get; set; }

        ICity City { get; set; }

        string PhoneNumber { get; set; }

        ICollection<IUser> FavouriteUsers { get; set; }

        byte[] Image { get; set; }

        string PersonalInfo { get; set; }

        ICollection<ISubscription> Subscriptions { get; set; }

        ICollection<IReview> GivenReviews { get; set; }

        ICollection<IReview> ReceivedReviews { get; set; }

        ICollection<IConversation> Conversations { get; set; }
    }
}
