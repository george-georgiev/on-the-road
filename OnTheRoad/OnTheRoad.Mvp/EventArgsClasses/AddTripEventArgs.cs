using System;
using System.Collections.Generic;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class AddTripEventArgs : EventArgs
    {
        public string TripName { get; set; }

        public string LoggedUserName { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<int> SelectedCategoryIds { get; set; }

        public IEnumerable<string> SelectedTagNames { get; set; }

        public byte[] CoverImageContent { get; set; }
    }
}
