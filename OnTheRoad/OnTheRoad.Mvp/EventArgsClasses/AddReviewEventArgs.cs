using System;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class AddReviewEventArgs :EventArgs
    {
        public string FromUser { get; set; }

        public string ToUser { get; set; }

        public RatingEnum Rating { get; set; }

        public string Content { get; set; }

        public DateTime PostingDate { get; set; }
    }
}
