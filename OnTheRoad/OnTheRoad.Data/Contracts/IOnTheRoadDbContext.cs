using System.Data.Entity;
using OnTheRoad.Data.Models;

namespace OnTheRoad.Data.Contracts
{
    public interface IOnTheRoadDbContext
    {
        IDbSet<Category> Categories { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Review> Reviews { get; set; }

        IDbSet<Subscription> Subscriptions { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Trip> Trips { get; set; }

        IDbSet<Rating> Ratings { get; set; }
    }
}
