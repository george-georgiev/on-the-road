using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class FavouriteUserEventArgs : EventArgs
    {
        public string FavouriteUserToRemove { get; set; }

        public string CurrentUsername { get; set; }
    }
}
