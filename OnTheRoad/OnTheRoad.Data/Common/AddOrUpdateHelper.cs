using System.Data.Entity.Migrations;
using OnTheRoad.Data.Contracts;

namespace OnTheRoad.Data.Common
{
    public class AddOrUpdateHelper : IAddOrUpdateHelper
    {
        public void AddOrUpdateEntity<TEntity>(IOnTheRoadDbContext db, params TEntity[] entities) where TEntity : class
        {
            db.Set<TEntity>().AddOrUpdate(entities);
        }
    }
}