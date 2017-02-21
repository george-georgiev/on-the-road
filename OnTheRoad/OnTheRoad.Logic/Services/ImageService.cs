using OnTheRoad.Logic.Contracts;
using System;

namespace OnTheRoad.Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageLoader imageLoader;
        private readonly IImagePathResolver imagePathResolver;
        private readonly IImageResizer imageResizer;

        public ImageService(IImageLoader imageLoader, IImagePathResolver imagePathResolver, IImageResizer imageResizer)
        {
            if (imageLoader == null)
            {
                throw new ArgumentNullException("imageLoader can not be null!");
            }

            if (imagePathResolver == null)
            {
                throw new ArgumentNullException("imagePathResolver can noe be null!");
            }

            if (imageResizer == null)
            {
                throw new ArgumentNullException("imageResizer can not be null!");
            }

            this.imageLoader = imageLoader;
            this.imagePathResolver = imagePathResolver;
            this.imageResizer = imageResizer;
        }
        public byte[] LoadResizedTripsImage()
        {
            var path = this.imagePathResolver.ResolveTripsImageFilePath();
            var image = this.imageLoader.LoadImage(path);

            var resized = this.imageResizer.ResizeImage(image);

            return resized;
        }
    }
}
