using System;
using System.Collections.Generic;
using System.Linq;
using OnTheRoad.Data.Common;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Factories;
using OnTheRoad.Data.Factories.Contracts;

namespace OnTheRoad.Data.Readers
{
    public class TextDataReader : IDataReader
    {
        private IResourcePathResolver resourcePathResolver;
        private IFileReaderFactory fileReaderFactory;


        public TextDataReader()
        {
            this.ResourcePathResolver = new ResourcePathResolver();
            this.FileReaderFactory = new FileReaderFactory();
        }

        public IFileReaderFactory FileReaderFactory
        {
            get
            {
                return this.fileReaderFactory;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("fileReaderFactory cannot be null!");
                }

                this.fileReaderFactory = value;
            }
        }

        public IResourcePathResolver ResourcePathResolver
        {
            get
            {
                return this.resourcePathResolver;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("resourcePathResolver cannot be null!");
                }

                this.resourcePathResolver = value;
            }
        }

        public IEnumerable<string> ReadCategories()
        {
            var result = new List<string>();
            string fileName = this.ResourcePathResolver.ResolveCategoriesFilePath();
            using (var stream = this.FileReaderFactory.GetStreamReader(fileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public IEnumerable<string> ReadCities()
        {
            var result = new List<string>();
            string fileName = this.ResourcePathResolver.ResolveCitiesFilePath();
            using (var stream = this.FileReaderFactory.GetStreamReader(fileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public IEnumerable<string> ReadRatings()
        {
            var result = new List<string>();
            string fileName = this.ResourcePathResolver.ResolveRatingsFilePath();
            using (var stream = this.FileReaderFactory.GetStreamReader(fileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
