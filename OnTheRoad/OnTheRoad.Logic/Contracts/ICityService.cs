using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface ICityService
    {
        IEnumerable<ICity> GetAllCities();
    }
}
