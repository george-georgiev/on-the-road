using System;
using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface ITrip
    {
        string Name { get; }

        string Description { get; }

        DateTime StartDate { get; }

        DateTime EndDate { get; }

        string Location { get; }

        ICollection<ITag> Tags { get; }

        ICollection<ICategory> Categories { get; }

        IImage Image { get; }
    }
}
