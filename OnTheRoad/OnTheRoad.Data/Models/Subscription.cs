using OnTheRoad.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.Data.Models
{
    public class Subscription : BaseEntity
    {
        [Required]
        public TripStatus Status { get; set; }

        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        // UserId
    }
}
