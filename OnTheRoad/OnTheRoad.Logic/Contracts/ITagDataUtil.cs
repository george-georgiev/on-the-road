using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITagDataUtil
    {
        ITag GetTagByName(string name);

        void AddTag(ITag tag);
    }
}
