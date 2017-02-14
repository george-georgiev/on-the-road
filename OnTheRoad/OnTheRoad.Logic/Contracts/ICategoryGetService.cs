using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ICategoryGetService
    {
        ICategory GetCategoryByName(string name);

        IEnumerable<ICategory> GetAllCategories();

        ICategory GetCategoryById(int id);

        IEnumerable<ICategory> GetCategoriesByIdCollection(IEnumerable<int> idCollection);
    }
}
