namespace OnTheRoad.Data.Contracts
{
    public interface IResourcePathResolver
    {
        string ResolveCategoriesFilePath();

        string ResolveCitiesFilePath();

        string ResolveRatingsFilePath();
    }
}
