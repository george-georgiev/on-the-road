using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface ICategoryService
    {
        void AddCategory(string name);

        ICategory GetCategoryByName(string name);
    }
}
