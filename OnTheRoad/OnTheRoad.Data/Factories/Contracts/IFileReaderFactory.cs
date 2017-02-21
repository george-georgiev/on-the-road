using System.IO;

namespace OnTheRoad.Data.Factories.Contracts
{
    public interface IFileReaderFactory
    {
        TextReader GetStreamReader(string fileName);
    }
}
