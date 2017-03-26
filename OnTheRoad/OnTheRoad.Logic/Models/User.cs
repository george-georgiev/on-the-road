using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class User : IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Info { get; set; }

        public ICity City { get; set; }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string PersonalInfo { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<IReview> GivenReviews { get; set; }

        public ICollection<IReview> ReceivedReviews { get; set; }

        public ICollection<ISubscription> Subscriptions { get; set; }

        public ICollection<IUser> FavouriteUsers { get; set; }

        public ICollection<IConversation> Conversations { get; set; }
    }
}
