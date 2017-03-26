using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Logic.Models
{
    public class Conversation : IConversation
    {
        public Conversation(IUser firstUser, IUser secondUser)
        {
            this.FirstUser = firstUser;
            this.SecondUser = secondUser;
            this.Messages = new List<IMessage>();
        }

        public int Id { get; set; }

        public IUser FirstUser { get; set; }

        public ICollection<IMessage> Messages { get; set; }

        public IUser SecondUser { get; set; }
    }
}
