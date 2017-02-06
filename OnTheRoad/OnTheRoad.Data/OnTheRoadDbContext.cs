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
    }
}
