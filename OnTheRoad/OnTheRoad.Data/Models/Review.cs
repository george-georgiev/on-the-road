using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Review : BaseEntity
    {
        public string FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        [InverseProperty("GivenReviews")]
        public virtual User FromUser { get; set; }

        public string ToUserId { get; set; }

        [ForeignKey("ToUserId")]
        [InverseProperty("ReceivedReviews")]
        public virtual User ToUser { get; set; }

        [ForeignKey("Rating")]
        public int RatingId { get; set; }

        public virtual Rating Rating { get; set; }

        public string ReviewContent { get; set; }

        public DateTime PostingDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
