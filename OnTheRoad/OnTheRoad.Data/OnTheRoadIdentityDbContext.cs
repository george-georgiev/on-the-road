using Microsoft.AspNet.Identity.EntityFramework;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OnTheRoad.Data
{
    public class OnTheRoadIdentityDbContext : IdentityDbContext<User>, IOnTheRoadDbContext
    {
        public OnTheRoadIdentityDbContext()
            : base("OnTheRoadDB", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Review> Reviews { get; set; }

        public virtual IDbSet<Subscription> Subscriptions { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Trip> Trips { get; set; }

        public virtual IDbSet<TripImage> TripImages { get; set; }
        
        public virtual IDbSet<UserImage> UserImages { get; set; }

        public static OnTheRoadIdentityDbContext Create()
        {
            return new OnTheRoadIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<User>().HasMany(m => m.FavouriteUsers).WithMany();
        }
    }
}
