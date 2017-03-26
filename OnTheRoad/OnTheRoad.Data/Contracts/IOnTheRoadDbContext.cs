using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using OnTheRoad.Data.Models;

namespace OnTheRoad.Data.Contracts
{
    public interface IOnTheRoadDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Review> Reviews { get; set; }

        IDbSet<Subscription> Subscriptions { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Trip> Trips { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Conversation> Conversations { get; set; }

        DbChangeTracker ChangeTracker { get; }

        DbContextConfiguration Configuration { get; }

        DbEntityEntry Entry(object entity);

        void SetEntryState(object entity, EntityState entityState);

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
