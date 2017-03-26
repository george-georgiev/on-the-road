using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.MVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConversationService conversationService;

        public ChatHub(IConversationService conversationService)
        {
            this.conversationService = conversationService;
        }

        public void SendMessage(string toUsername, string text)
        {
            var fromUsername = this.Context.User.Identity.Name;
            this.conversationService.SendMessage(text, fromUsername, toUsername);

            this.Clients.Group(toUsername).addMessage(text, fromUsername);
            this.Clients.Group(fromUsername).addMessage(text, fromUsername);
        }

        public override Task OnConnected()
        {
            string name = this.Context.User.Identity.Name;

            this.Groups.Add(this.Context.ConnectionId, name);

            return base.OnConnected();  
        }
    }
}