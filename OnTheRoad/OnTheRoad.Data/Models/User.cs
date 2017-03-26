using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnTheRoad.Data.Models
{
    public class User : IdentityUser
    {
        private ICollection<Review> givenReviews;
        private ICollection<Review> receivedReviews;
        private ICollection<User> favouriteUsers;
        private ICollection<Subscription> subscriptions;
        private ICollection<Conversation> conversations;

        public User()
        {
            this.GivenReviews = new HashSet<Review>();
            this.ReceivedReviews = new HashSet<Review>();
            this.FavouriteUsers = new HashSet<User>();
            this.Subscriptions = new HashSet<Subscription>();
            this.Conversations = new HashSet<Conversation>();
        }

        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public string Info { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Review> GivenReviews
        {
            get { return this.givenReviews; }
            set { this.givenReviews = value; }
        }

        public virtual ICollection<Review> ReceivedReviews
        {
            get { return this.receivedReviews; }
            set { this.receivedReviews = value; }
        }

        public virtual ICollection<User> FavouriteUsers
        {
            get { return this.favouriteUsers; }
            set { this.favouriteUsers = value; }
        }

        public virtual ICollection<Subscription> Subscriptions
        {
            get { return this.subscriptions; }
            set { this.subscriptions = value; }
        }

        public virtual ICollection<Conversation> Conversations
        {
            get { return this.conversations; }
            set { this.conversations = value; }
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("UserId", this.Id));
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(this.GenerateUserIdentity(manager));
        }
    }
}
