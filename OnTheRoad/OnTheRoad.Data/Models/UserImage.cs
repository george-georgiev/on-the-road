using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.Data.Models
{
    public class UserImage: BaseEntity
    {
        [Required]
        public string Path { get; set; }
    }
}
