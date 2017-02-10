using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class FavouriteUserEventArgs : EventArgs
    {
        public string FavouriteUserUsername { get; set; }

        public string CurrentUserUsername { get; set; }
    }
}
