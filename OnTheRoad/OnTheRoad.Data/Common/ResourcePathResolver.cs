using System;
using System.Configuration;
using System.IO;
using OnTheRoad.Data.Contracts;

namespace OnTheRoad.Data.Common
{
    public class ResourcePathResolver : IResourcePathResolver
    {
        private const string ResourceDirectory = @"..\..\Resources\";
        private const string CategoriesAppSetting = "CategoriesSeedFile";
        private const string CitiesAppSetting = "CitiesSeedFile";
        private const string RatingsAppSetting = "RatingsSeedFile";

        public string ResolveCategoriesFilePath()
        {
            var filePath = ConfigurationManager.AppSettings[CategoriesAppSetting];
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceDirectory, filePath);

            return filePath;
        }

        public string ResolveCitiesFilePath()
        {
            var filePath = ConfigurationManager.AppSettings[CitiesAppSetting];
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceDirectory, filePath);

            return filePath;
        }

        public string ResolveRatingsFilePath()
        {
            var filePath = ConfigurationManager.AppSettings[RatingsAppSetting];
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceDirectory, filePath);

            return filePath;
        }
    }
}
