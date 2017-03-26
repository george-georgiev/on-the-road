using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Domain.Repositories
{
    public interface IConversationRepository : IGetRepository<IConversation>, IModifyRepository<IConversation>
    {
        IEnumerable<IConversation> GetAllUserConversations(string username);

        IConversation GetConversationForUsers(string firstUsername, string secondUsername);

        bool DoesConversationForUsersExist(string firstUsername, string secondUsername);
    }
}
