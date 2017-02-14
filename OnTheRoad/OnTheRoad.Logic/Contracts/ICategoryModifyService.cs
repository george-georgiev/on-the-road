namespace OnTheRoad.Logic.Contracts
{
    public interface ICategoryModifyService
    {
        void AddCategory(string name);

        void DeleteCategoryByName(string name);
    }
}
