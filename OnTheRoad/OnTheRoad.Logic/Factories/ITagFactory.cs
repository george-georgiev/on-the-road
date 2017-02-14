using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Factories
{
    public interface ITagFactory
    {
        ITag CreateTag(string name);
    }
}
