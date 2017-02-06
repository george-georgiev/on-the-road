using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.Data.Models
{
   public class TripImage:BaseEntity
    {
        [Required]
        public string Path { get; set; }
    }
}
