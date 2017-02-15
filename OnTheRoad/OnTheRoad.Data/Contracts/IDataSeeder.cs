namespace OnTheRoad.Data.Contracts
{
    public interface IDataSeeder
    {
        void SeedCategories(IOnTheRoadDbContext context);

        void SeedCities(IOnTheRoadDbContext context);

        void SeedRating(IOnTheRoadDbContext context);
    }
}
