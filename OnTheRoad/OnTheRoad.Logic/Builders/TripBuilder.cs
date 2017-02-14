using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Models;

namespace OnTheRoad.Logic.Builders
{
    public class TripBuilder : ITripBuilder
    {
        private string name;
        private string description;
        private string location;
        private DateTime startDate;
        private DateTime endDate;
        private ICollection<ICategory> categories;
        private ICollection<ITag> tags;
        private IImage image;

        public TripBuilder()
        {
            this.name = null;
            this.description = null;
            this.location = null;
            this.startDate = default(DateTime);
            this.endDate = default(DateTime);
            this.categories = null;
            this.tags = null;
            this.image = null;
        }

        public ITrip Build()
        {
            var trip = new Trip(name, description, location, startDate, endDate, categories, tags, image);

            return trip;
        }

        public ITripBuilder SetCategories(ICollection<ICategory> categories)
        {
            this.categories = categories;
            return this;
        }

        public ITripBuilder SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        public ITripBuilder SetEndDate(DateTime endDate)
        {
            this.endDate = endDate;
            return this;
        }

        public ITripBuilder SetImage(IImage image)
        {
            this.image = image;
            return this;
        }

        public ITripBuilder SetLocation(string location)
        {
            this.location = location;
            return this;
        }

        public ITripBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }

        public ITripBuilder SetStartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }

        public ITripBuilder SetTags(ICollection<ITag> tags)
        {
            this.tags = tags;
            return this;
        }
    }
}
