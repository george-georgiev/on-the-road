using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Areas.User.Models;
using OnTheRoad.MVC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.User.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IConversationService conversationService;

        public MessagesController(IConversationService conversationService)
        {
            if (conversationService == null)
            {
                throw new ArgumentNullException("conversationService can not be null!");
            }

            this.conversationService = conversationService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index(string username)
        {
            var loggedUsername = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var isOwner = username == loggedUsername;

            var model = new MessagesViewModel();
            model.IsOwner = isOwner;

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Conversations()
        {
            var loggedUsername = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var conversations = this.conversationService.GetConversationsForUser(loggedUsername);

            var model = new List<ConversationViewModel>();
            foreach (var conversation in conversations)
            {
                var mapped = MapperProvider.Mapper.Map<ConversationViewModel>(conversation);
                var lastMessage = conversation.Messages.OrderByDescending(x => x.CreateDate).Take(1).FirstOrDefault().Text;
                mapped.LastMessage = lastMessage;
                model.Add(mapped);
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Chat(string username)
        {
            var loggedUsername = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var conversationExist = this.conversationService.DoesConversationForUsersExist(loggedUsername, username);

            IEnumerable<MessageViewModel> mappedMessages;
            if (conversationExist)
            {
                var conversation = this.conversationService.GetConversationForUsers(loggedUsername, username);
                var conversationMessages = conversation.Messages;

                mappedMessages = MapperProvider.Mapper.Map<IEnumerable<MessageViewModel>>(conversationMessages);
            }
            else
            {
                mappedMessages = new List<MessageViewModel>();
            }

            var model = new ChatViewModel();
            model.Messages = mappedMessages;

            model.TargetUser = username;

            return PartialView(model);
        }
    }
}