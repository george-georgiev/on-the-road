using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.Data.Models
{
    public class Trip : BaseEntity
    {
        private ICollection<Category> categories;

        private ICollection<Tag> tags;

        public Trip()
        {
            this.categories = new HashSet<Category>();
            this.tags = new HashSet<Tag>();
        }

        [Required]
        public string Name { get; set; }

        public int TripImageId { get; set; }

        public virtual TripImage TripImage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        // Collection?
        public int SubscriptionId { get; set; }

        public virtual Subscription Subscription { get; set; }
    }
}
