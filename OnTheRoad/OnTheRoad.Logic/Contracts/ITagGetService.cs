using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITagGetService
    {
        IEnumerable<ITag> GetTagsByNamePrefix(string prefix, int take);
    }
}
