using System.Collections.Generic;

namespace OnTheRoad.Domain.Repositories
{
    public interface IRepository<DomainType>
        where DomainType : class
    {
        IEnumerable<DomainType> GetAll();

        DomainType GetById(object id);

        void Add(DomainType entity);

        void Delete(DomainType entity);

        void Update(DomainType entity);
    }
}
