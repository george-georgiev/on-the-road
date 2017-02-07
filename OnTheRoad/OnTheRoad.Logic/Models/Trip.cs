using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Models
{
    public class Trip : ITrip, IIdentifiable
    {
        public Trip(string name)
        {
            this.Name = name;
        }

        public ICollection<ICategory> Categories { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public int Id { get; set; }

        public IImage Image { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<ITag> Tags { get; set; }
    }
}
