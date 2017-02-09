using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IModifyRepository<DomainType>
         where DomainType : IIdentifiable
    {
        void Add(DomainType entity);

        void Delete(DomainType entity);

        void Update(DomainType entity);
    }
}
