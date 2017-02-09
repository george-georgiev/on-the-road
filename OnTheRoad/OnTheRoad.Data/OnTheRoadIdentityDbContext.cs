using Microsoft.AspNet.Identity.EntityFramework;
using OnTheRoad.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OnTheRoad.Data
{
    public class OnTheRoadIdentityDbContext : IdentityDbContext<User>
    {
        public OnTheRoadIdentityDbContext()
            : base("OnTheRoadDB", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Trip> Trips { get; set; }

        public virtual DbSet<TripImage> TripImages { get; set; }
        
        public virtual DbSet<UserImage> UserImages { get; set; }

        public static OnTheRoadIdentityDbContext Create()
        {
            return new OnTheRoadIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>().HasMany(m => m.Reviews).WithMany();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
        }
    }
}
