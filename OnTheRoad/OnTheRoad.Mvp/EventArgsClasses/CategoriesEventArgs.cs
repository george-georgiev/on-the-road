using System;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class CategoriesEventArgs : EventArgs
    {
        public string CategoryName { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
