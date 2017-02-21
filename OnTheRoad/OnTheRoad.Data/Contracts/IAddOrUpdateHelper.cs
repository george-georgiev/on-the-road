namespace OnTheRoad.Data.Contracts
{
    public interface IAddOrUpdateHelper
    {
        void AddOrUpdateEntity<TEntity>(IOnTheRoadDbContext db, params TEntity[] entities) where TEntity : class;
    }
}
