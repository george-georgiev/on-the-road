using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class ImageUploadEventArgs : EventArgs
    {
        public byte[] ImageContent { get; set; }

        public string FileName { get; set; }
    }
}
