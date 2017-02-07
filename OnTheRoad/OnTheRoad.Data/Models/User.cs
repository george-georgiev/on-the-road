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
        private ICollection<Review> reviews;
        private ICollection<User> favouriteUsers;
        private ICollection<Subscription> subscription;

        public User()
        {
            this.reviews = new HashSet<Review>();
            this.favouriteUsers = new HashSet<User>();
            this.subscription = new HashSet<Subscription>();
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

        [ForeignKey("Image")]
        public int? ImageId { get; set; }

        public virtual UserImage Image { get; set; }

        public virtual ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }

        public virtual ICollection<User> FavouriteUsers
        {
            get { return this.favouriteUsers; }
            set { this.favouriteUsers = value; }
        }

        public virtual ICollection<Subscription> Subscriptions
        {
            get { return this.subscription; }
            set { this.subscription = value; }
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
