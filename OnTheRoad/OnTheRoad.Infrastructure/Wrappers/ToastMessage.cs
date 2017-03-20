using OnTheRoad.Infrastructure.Enums;
using System;

namespace OnTheRoad.Infrastructure.Wrappers
{
    [Serializable]
    public class ToastMessage
    {
        public ToastMessage(string title, string message, ToastType toastType)
        {
            this.Title = title;
            this.Message = message;
            this.ToastType = toastType;
        }

        public string Title { get; }

        public string Message { get; }

        public ToastType ToastType { get; }
    }
}
