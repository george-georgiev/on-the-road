using OnTheRoad.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Review: BaseEntity
    {
        [ForeignKey("FromUser")]
        public string FromUserId { get; set; }

        public virtual User FromUser { get; set; }

        [ForeignKey("ToUser")]
        public string ToUserId { get; set; }

        public virtual User ToUser { get; set; }

        public Rating Rating { get; set; }

        public string Comment { get; set; }
    }
}
