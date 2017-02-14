using System;
using System.IO;

namespace OnTheRoad.CustomControllers
{
    public partial class FileUploader : System.Web.UI.UserControl
    {
        public string ErrorStatus { get; set; }

        public string FilePath { get; set; }

        public string GetFilePath { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            var fileUpload = this.FileUploadImage;

            if (fileUpload.HasFile)
            {
                try
                {
                    if (fileUpload.PostedFile.ContentType == "image/jpeg" || fileUpload.PostedFile.ContentType == "image/png")
                    {
                        if (fileUpload.PostedFile.ContentLength < 4 * 1000 * 1024)
                        {
                            string filename = Path.GetFileName(fileUpload.FileName);
                            byte[] fileData = null;
                            Stream fileStream = null;
                            int length = 0;
                            length = fileUpload.PostedFile.ContentLength;
                            fileData = new byte[length + 1];
                            fileStream = fileUpload.PostedFile.InputStream;
                            fileStream.Read(fileData, 0, length);

                            // save to user
                         

                            // Old way - writting to file.
                            //string root = Server.MapPath("~");
                            //string parent = Path.GetDirectoryName(root);
                            //string grandParent = Path.GetDirectoryName(parent);
                            //fileUpload.SaveAs(grandParent + this.FilePath + filename);
                            //this.GetFilePath = grandParent + @"\FileUploads\UserImages\" + filename;
                            this.ErrorStatus = "";
                        }
                        else
                        {
                            this.ErrorStatus = "Снимката трябва да е до 4MB!";
                        }
                    }
                    else
                    {
                        this.ErrorStatus = "Само JPEG И PNG файлове може да бъдат качвани!";
                    }
                }
                catch (Exception)
                {
                    this.ErrorStatus = "Възникна грешка при качването. Моля опитайте отново.";
                }
            }
        }
    }
}