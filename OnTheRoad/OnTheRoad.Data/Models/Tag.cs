using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.Data.Models
{
    public class Tag : BaseEntity
    {
        private ICollection<Trip> trip;

        public Tag()
        {
            this.trip = new HashSet<Trip>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trip; }
            set { this.trip = value; }
        }
    }
}
