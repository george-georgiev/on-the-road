using System.Collections.Generic;

namespace OnTheRoad.Data.Contracts
{
    public interface IDataReader
    {
        IEnumerable<string> ReadCategories();
    }
}
