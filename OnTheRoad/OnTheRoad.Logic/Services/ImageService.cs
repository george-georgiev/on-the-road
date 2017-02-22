using OnTheRoad.Logic.Contracts;
using System;

namespace OnTheRoad.Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageLoader imageLoader;
        private readonly IImagePathResolver imagePathResolver;

        public ImageService(IImageLoader imageLoader, IImagePathResolver imagePathResolver)
        {
            if (imageLoader == null)
            {
                throw new ArgumentNullException("imageLoader can not be null!");
            }

            if (imagePathResolver == null)
            {
                throw new ArgumentNullException("imagePathResolver can noe be null!");
            }

            this.imageLoader = imageLoader;
            this.imagePathResolver = imagePathResolver;
        }

        public byte[] LoadTripsImage()
        {
            var path = this.imagePathResolver.ResolveTripsImageFilePath();
            var image = this.imageLoader.LoadImage(path);

            return image;
        }
    }
}
