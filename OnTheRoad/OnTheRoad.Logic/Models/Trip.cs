using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Models
{
    public class Trip : ITrip, IIdentifiable
    {
        public Trip(
            string name,
            string description,
            string location,
            DateTime startDate,
            DateTime endDate,
            ICollection<ICategory> categories,
            ICollection<ITag> tags,
            byte[] coverImage)
        {
            this.Name = name;
            this.Description = description;
            this.Location = location;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Categories = categories;
            this.Tags = tags;
            this.CoverImage = coverImage;
        }

        public ICollection<ICategory> Categories { get; set; }

        //public IImage CoverImage { get; set; }

        public byte[] CoverImage { get; set; }

        public DateTime CreateDate { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public int Id { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public IUser Organiser { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<ISubscription> Subscriptions { get; set; }

        public ICollection<ITag> Tags { get; set; }
    }
}
