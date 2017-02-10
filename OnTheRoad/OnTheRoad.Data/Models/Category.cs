using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Category : BaseEntity
    {
        private ICollection<Trip> trips;

        public Category()
        {
            this.Trips = new HashSet<Trip>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }
    }
}
