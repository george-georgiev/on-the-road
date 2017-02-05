using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface ICategoryRepository<DomainType> : IRepository<DomainType>
        where DomainType : class
    {
        ICategory GetCategoryByName(string name);
    }
}
