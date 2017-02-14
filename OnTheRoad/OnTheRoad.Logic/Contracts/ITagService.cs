using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITagService : ITagGetService
    {
        IEnumerable<ITag> GetOrCreateTags(IEnumerable<string> tagNames);
    }
}
