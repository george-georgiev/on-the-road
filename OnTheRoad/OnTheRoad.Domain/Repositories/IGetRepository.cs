using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Domain.Repositories
{
    public interface IGetRepository<DomainType>
        where DomainType : IIdentifiable
    {
        IEnumerable<DomainType> GetAll();

        DomainType GetById(object id);
    }
}
