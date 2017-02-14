namespace OnTheRoad.Data.Contracts
{
    public interface IDataSeeder
    {
        void SeedCategories(IOnTheRoadDbContext context);
    }
}
