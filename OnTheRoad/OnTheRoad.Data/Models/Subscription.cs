using OnTheRoad.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Subscription : BaseEntity
    {
        [Required]
        public SubscriptionStatus Status { get; set; }

        [Required]
        [ForeignKey("Trip")]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
