﻿using ImageResizer;
using OnTheRoad.Mvp.EventArgsClasses;
using System;
using System.IO;

namespace OnTheRoad.CustomControllers
{
    public partial class ImageUploader : System.Web.UI.UserControl
    {
        private const int FourMb = 4 * 1000 * 1024;
        private const string JpegContentType = "image/jpeg";
        private const string ImageContentType = "image/png";

        private const string ImageNotSelectedError = "Моля изберете файл";
        private const string ImageExtentionError = "Само JPEG И PNG файлове могат да бъдат качвани!";
        private const string ImageSizeError = "Снимката трябва да е до 4MB!";
        private const string ImageUploadGeneralError = "Възникна грешка при качването. Моля опитайте отново.";

        private const string InstructionsQueryString = "width=600;format=jpg;mode=max";

        public event EventHandler<ImageUploadEventArgs> ImageUpload;
        public event EventHandler<ErrorEventArgs> Error;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FileUploadImage.HasFile)
                {
                    this.RaiseErrorEvent(ImageNotSelectedError);
                }
                else if (this.FileUploadImage.PostedFile.ContentType != JpegContentType && this.FileUploadImage.PostedFile.ContentType != ImageContentType)
                {
                    this.RaiseErrorEvent(ImageExtentionError);
                }
                else if (this.FileUploadImage.PostedFile.ContentLength > FourMb)
                {
                    this.RaiseErrorEvent(ImageSizeError);
                }
                else
                {
                    this.RaiseImageUploadEvent();
                }
            }
            catch (Exception)
            {
                this.RaiseErrorEvent(ImageUploadGeneralError);
            }
        }

        private void RaiseErrorEvent(string errorMessage)
        {
            var exception = new InvalidOperationException(errorMessage);
            this.Error?.Invoke(this, new ErrorEventArgs(exception));
        }

        private void RaiseImageUploadEvent()
        {
            var filename = Path.GetFileName(this.FileUploadImage.FileName);
            var fileStream = this.FileUploadImage.PostedFile.InputStream;
            byte[] imageAsByteArray = null;

            using (var ms = new MemoryStream())
            {
                var instructions = new Instructions(InstructionsQueryString);
                var imageJob = new ImageJob(fileStream, ms, instructions);
                imageJob.Build();
                imageAsByteArray = ms.ToArray();
            }

            this.ImageUpload?.Invoke(this, new ImageUploadEventArgs()
            {
                Image = imageAsByteArray,
                FileName = this.FileUploadImage.FileName
            });

            this.LabelErrors.Text = string.Empty;
        }
    }
}