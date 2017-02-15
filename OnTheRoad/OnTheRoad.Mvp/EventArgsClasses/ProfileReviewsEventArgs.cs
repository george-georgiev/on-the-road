using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Mvp.EventArgsClasses
{
    public class ProfileReviewsEventArgs : EventArgs
    {
        public string FromUser { get; set; }

        public string ToUser { get; set; }

        public string Rating { get; set; }

        public string Content { get; set; }

        public IEnumerable<IReview> Reviews { get; set; }
    }
}
