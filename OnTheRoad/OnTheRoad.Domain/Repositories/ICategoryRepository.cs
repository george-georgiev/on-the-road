using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface ICategoryRepository : IGetRepository<ICategory>, IModifyRepository<ICategory>
    {
        ICategory GetCategoryByName(string name);
    }
}
