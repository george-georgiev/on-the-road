using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Conversation : BaseEntity
    {
        private ICollection<Message> messages;

        public Conversation()
        {
            this.Messages = new HashSet<Message>();
        }

        [ForeignKey("FirstUser")]
        public string FirstUserId { get; set; }

        public virtual User FirstUser { get; set; }

        [ForeignKey("SecondUser")]
        public string SecondUserId { get; set; }

        public virtual User SecondUser { get; set; }

        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }
    }
}
