using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Domain.Repositories
{
    public interface ITagRepository : IGetRepository<ITag>, IModifyRepository<ITag>
    {
        ITag GetTagByName(string name);

        IEnumerable<ITag> GetTagsByPrefix(string prefix);
    }
}
