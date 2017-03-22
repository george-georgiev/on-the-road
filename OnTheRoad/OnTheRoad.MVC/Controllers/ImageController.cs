using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.Infrastructure.Json;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Filters;
using System;
using System.Web;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class ImageController : Controller
    {
        private const int FourMb = 4 * 1000 * 1024;
        private const string JpegContentType = "image/jpeg";
        private const string PngContentType = "image/png";

        private readonly IImageResizer imageResizer;

        public ImageController(IImageResizer imageResizer)
        {
            if (imageResizer == null)
            {
                throw new ArgumentNullException("imageResizer cannot be null!");
            }

            this.imageResizer = imageResizer;
        }

        [Authorize]
        [HttpPost]
        [Ajax]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            Result result;
            try
            {
                if (file.ContentLength == 0)
                {
                    result = new Result(Resources.Messages.FileNotSelectedError, ResponseStatus.BadRequest);
                    this.Response.StatusCode = (int)ResponseStatus.BadRequest;
                }
                else if (file.ContentType != JpegContentType && file.ContentType != PngContentType)
                {
                    result = new Result(Resources.Messages.ImageExtensionsOnlyAllowedError, ResponseStatus.BadRequest);
                    this.Response.StatusCode = (int)ResponseStatus.BadRequest;
                }
                else if (file.ContentLength > FourMb)
                {
                    result = new Result(Resources.Messages.ImageSizeError, ResponseStatus.BadRequest);
                    this.Response.StatusCode = (int)ResponseStatus.BadRequest;
                }
                else
                {
                    var stream = file.InputStream;
                    var resizedStream = this.imageResizer.ResizeImage(stream);

                    result = new Result(string.Empty, ResponseStatus.Ok, Convert.ToBase64String(resizedStream));
                }
            }
            catch (Exception)
            {
                result = new Result(Resources.Messages.ImageUploadError, ResponseStatus.ServerError);
                this.Response.StatusCode = (int)ResponseStatus.ServerError;
            }

            return this.Json(result);
        }
    }
}