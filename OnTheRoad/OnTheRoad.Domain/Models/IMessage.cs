using System;

namespace OnTheRoad.Domain.Models
{
    public interface IMessage : IIdentifiable
    {
        IUser Author { get; set; }

        string Text { get; set; }

        bool IsDeleted { get; set; }

        bool IsEdited { get; set; }

        DateTime CreateDate { get; set; }

        IConversation Conversation { get; set; }
    }
}
