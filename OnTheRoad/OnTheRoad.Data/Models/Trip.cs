using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Trip : BaseEntity
    {
        private ICollection<Category> categories;
        private ICollection<Tag> tags;
        private ICollection<Subscription> subscription;

        public Trip()
        {
            this.categories = new HashSet<Category>();
            this.tags = new HashSet<Tag>();
            this.subscription = new HashSet<Subscription>();
        }

        [Required]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        [ForeignKey("CoverImage")]
        public int? CoverImageId { get; set; }

        public virtual TripImage CoverImage { get; set; }

        [ForeignKey("Organiser")]
        public string OrganiserId { get; set; }

        public virtual User Organiser { get; set; }

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

        public virtual ICollection<Subscription> Subscriptions
        {
            get { return this.subscription; }
            set { this.subscription = value; }
        }
    }
}
