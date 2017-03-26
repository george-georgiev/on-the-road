using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Message : BaseEntity
    {
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string Text { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsEdited { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
