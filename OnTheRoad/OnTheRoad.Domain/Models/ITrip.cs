using System;
using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface ITrip : IIdentifiable
    {
        string Name { get; set; }

        string Description { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        string Location { get; set; }

        ICollection<ITag> Tags { get; set; }

        ICollection<ICategory> Categories { get; set; }

        IImage Image { get; set; }
    }
}
