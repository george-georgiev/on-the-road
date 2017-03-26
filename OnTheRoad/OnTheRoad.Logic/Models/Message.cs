using OnTheRoad.Domain.Models;
using System;

namespace OnTheRoad.Logic.Models
{
    public class Message : IMessage
    {
        public Message(IUser author, IConversation conversation, DateTime createDate, string text)
        {
            this.Author = author;
            this.Conversation = conversation;
            this.CreateDate = createDate;
            this.Text = text;
            this.IsDeleted = false;
            this.IsEdited = false;
        }

        public int Id { get; set; }

        public IUser Author { get; set; }

        public IConversation Conversation { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsEdited { get; set; }

        public string Text { get; set; }
    }
}
