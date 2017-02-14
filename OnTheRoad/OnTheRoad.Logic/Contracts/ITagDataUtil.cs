using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITagDataUtil
    {
        ITag GetTagByName(string name);

        void AddTag(ITag tag);

        IEnumerable<ITag> GetTagsByNamePrefix(string prefix, int take);
    }
}
