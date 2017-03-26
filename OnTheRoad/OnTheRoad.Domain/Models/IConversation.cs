using System.Collections.Generic;

namespace OnTheRoad.Domain.Models
{
    public interface IConversation : IIdentifiable
    {
        IUser FirstUser { get; set; }

        IUser SecondUser { get; set; }

        ICollection<IMessage> Messages { get; set; }
    }
}
