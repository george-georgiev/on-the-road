using ImageResizer;
using OnTheRoad.Logic.Contracts;
using System.IO;

namespace OnTheRoad.Common
{
    public class CustomImageResizer : IImageResizer
    {
        private const string InstructionsQueryString = "width=600;format=jpg;mode=max";

        public byte[] ResizeImage(byte[] fileStream)
        {
            byte[] imageAsByteArray = null;

            using (var ms = new MemoryStream())
            {
                var instructions = new Instructions(InstructionsQueryString);
                var imageJob = new ImageJob(fileStream, ms, instructions);
                imageJob.Build();
                imageAsByteArray = ms.ToArray();
            }

            return imageAsByteArray;
        }
    }
}