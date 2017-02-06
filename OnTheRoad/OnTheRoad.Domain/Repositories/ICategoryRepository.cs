using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<ICategory>
    {
        ICategory GetCategoryByName(string name);
    }
}
