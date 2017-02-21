using System.IO;
using OnTheRoad.Data.Factories.Contracts;

namespace OnTheRoad.Data.Factories
{
    public class FileReaderFactory : IFileReaderFactory
    {
        public TextReader GetStreamReader(string fileName)
        {
            return new StreamReader(fileName);
        }
    }
}
