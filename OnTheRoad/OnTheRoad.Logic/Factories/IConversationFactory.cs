using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Factories
{
    public interface IConversationFactory
    {
        IConversation CreateConversation(IUser firstUser, IUser secondUser);
    }
}
