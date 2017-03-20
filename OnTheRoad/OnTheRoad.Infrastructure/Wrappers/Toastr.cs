using OnTheRoad.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace OnTheRoad.Infrastructure.Wrappers
{
    [Serializable]
    public class Toastr
    {
        public Toastr()
        {
            this.ToastMessages = new List<ToastMessage>();
            this.ShowNewestOnTop = false;
            this.ShowCloseButton = false;
        }

        public bool ShowNewestOnTop { get; set; }

        public bool ShowCloseButton { get; set; }

        public ICollection<ToastMessage> ToastMessages { get; }

        public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
        {
            var toast = new ToastMessage(title, message, toastType);
            this.ToastMessages.Add(toast);

            return toast;
        }
    }
}
