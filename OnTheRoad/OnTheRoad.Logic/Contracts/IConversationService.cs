using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Contracts
{
    public interface IConversationService
    {
        void SendMessage(string text, string fromUsername, string toUsername);

        IConversation GetConversationForUsers(string firstUsername, string secondUsername);

        bool DoesConversationForUsersExist(string firstUsername, string secondUsername);

        IEnumerable<IConversation> GetConversationsForUser(string username);
    }
}
