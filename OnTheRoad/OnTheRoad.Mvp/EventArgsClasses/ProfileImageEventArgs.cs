using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class ProfileImageEventArgs: EventArgs
    {
        public byte[] Image { get; set; }

        public string UserName { get; set; }
    }
}
