using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface ITripBuilder
    {
        ITripBuilder SetName(string name);

        ITripBuilder SetDescription(string description);

        ITripBuilder SetLocation(string location);

        ITripBuilder SetStartDate(DateTime startDate);

        ITripBuilder SetEndDate(DateTime endDate);

        ITripBuilder SetCategories(ICollection<ICategory> categories);

        ITripBuilder SetTags(ICollection<ITag> tags);

        ITripBuilder SetImage(byte[] image);

        ITrip Build();
    }
}
