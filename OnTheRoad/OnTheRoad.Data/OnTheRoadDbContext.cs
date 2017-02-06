using System.Data.Entity;
using OnTheRoad.Data.Models;

namespace OnTheRoad.Data
{
    public class OnTheRoadDbContext : DbContext
    {
        public OnTheRoadDbContext() : base("OnTheRoadDB")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        
        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Trip> Trips { get; set; }

        public virtual DbSet<TripImage> TripImages { get; set; }

        //public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<UserImage> UserImages { get; set; }
    }
}
