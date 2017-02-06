using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ICategoryService
    {
        void AddCategory(string name);

        ICategory GetCategoryByName(string name);

        void DeleteCategoryByName(string name);

        IEnumerable<ICategory> GetAllCategories();

        ICategory GetCategoryById(int id);
    }
}
