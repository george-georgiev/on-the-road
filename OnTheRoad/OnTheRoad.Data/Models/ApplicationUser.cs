using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using OnTheRoad.Identity;

namespace OnTheRoad.Data.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Review> reviews;
        private ICollection<ApplicationUser> favouriteUsers;

        public ApplicationUser()
        {
            this.reviews = new HashSet<Review>();
            this.favouriteUsers = new HashSet<ApplicationUser>();
        }

        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int ImageId { get; set; }

        public UserImage UserImage { get; set; }

        public string Info { get; set; }

        public int SubscriptionId { get; set; }

        public virtual Subscription Subscription { get; set; }

        public ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }

        public ICollection<ApplicationUser> FavouriteUsers
        {
            get { return this.favouriteUsers; }
            set { this.favouriteUsers = value; }
        }

        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("UserId", this.Id));
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            return Task.FromResult(this.GenerateUserIdentity(manager));
        }
    }
}
