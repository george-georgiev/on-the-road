using OnTheRoad.Logic.Contracts;
using System;
using System.Configuration;
using System.IO;

namespace OnTheRoad.Logic.Utils
{
    public class ImagePathResolver : IImagePathResolver
    {
        private const string ImagesDirectory = @"Content\Images\";
        private const string TripsDefaultImageAppSetting = "TripsDefaultImage";

        public string ResolveTripsImageFilePath()
        {
            var filePath = ConfigurationManager.AppSettings[TripsDefaultImageAppSetting];
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagesDirectory, filePath);

            return filePath;
        }
    }
}
