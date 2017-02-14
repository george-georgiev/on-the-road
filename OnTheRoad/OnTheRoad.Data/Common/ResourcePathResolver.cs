using OnTheRoad.Data.Contracts;
using System;
using System.Configuration;
using System.IO;

namespace OnTheRoad.Data.Common
{
    public class ResourcePathResolver : IResourcePathResolver
    {
        private const string ResourceDirectory = @"..\..\Resources\";
        private const string CategoriesAppSetting = "CategoriesSeedFile";

        public string ResolveCategoriesFilePath()
        {
            var filePath = ConfigurationManager.AppSettings[CategoriesAppSetting];
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceDirectory, filePath);

            return filePath;
        }
    }
}
