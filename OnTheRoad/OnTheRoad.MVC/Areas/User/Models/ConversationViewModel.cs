using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class ConversationViewModel : IMapFrom<IConversation>
    {
        public string FirstUserUsername { get; set; }

        public byte[] FirstUserImage { get; set; }

        public string SecondUserUsername { get; set; }

        public byte[] SecondUserImage { get; set; }

        public string LastMessage { get; set; }

        //public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}