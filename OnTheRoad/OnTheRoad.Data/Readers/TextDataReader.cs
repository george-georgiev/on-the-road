using OnTheRoad.Data.Common;
using OnTheRoad.Data.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnTheRoad.Data.Readers
{
    public class TextDataReader : IDataReader
    {
        private IResourcePathResolver resourcePathResolver;

        public TextDataReader()
        {
            this.ResourcePathResolver = new ResourcePathResolver();
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
                    throw new ArgumentNullException("resourcePathResolver can not be null!");
                }

                this.resourcePathResolver = value;
            }
        }

        public IEnumerable<string> ReadCategories()
        {
            var result = new List<string>();
            string fileName = this.ResourcePathResolver.ResolveCategoriesFilePath();
            using (var stream = new StreamReader(fileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    result.AddRange(line.Split(' ').ToList());
                }
            }

            return result;
        }
    }
}
