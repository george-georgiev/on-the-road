using OnTheRoad.Logic.Contracts;
using System;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Factories;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Logic.Utils
{
    public class ConversationHelper : IConversationHelper
    {
        private readonly IConversationFactory conversatioFactory;
        private readonly IMessageFactory messageFactory;
        private readonly IUserGetService userGetService;
        private readonly IMessageRepository messageRepository;

        public ConversationHelper(IConversationFactory conversatioFactory, IUserGetService userGetService, IMessageFactory messageFactory, IMessageRepository messageRepository)
        {
            if (conversatioFactory == null)
            {
                throw new ArgumentNullException("conversatioFactory can not be null!");
            }

            if (userGetService == null)
            {
                throw new ArgumentNullException("userGetService can not be null!");
            }

            if (messageFactory == null)
            {
                throw new ArgumentNullException("messageFactory can not be null!");
            }

            if (messageRepository == null)
            {
                throw new ArgumentNullException("messageRepository can not be null!");
            }

            this.conversatioFactory = conversatioFactory;
            this.userGetService = userGetService;
            this.messageFactory = messageFactory;
            this.messageRepository = messageRepository;
        }

        public IConversation CreateConversation(string firstUsername, string secondUername)
        {
            var firstUser = this.userGetService.GetUserInfo(firstUsername);
            var secondUser = this.userGetService.GetUserInfo(secondUername);

            var conversation = this.conversatioFactory.CreateConversation(firstUser, secondUser);

            return conversation;
        }

        public void AddMessage(IConversation conversation, string text, string authorUsername)
        {
            var author = this.userGetService.GetUserInfo(authorUsername);
            var createDate = DateTime.Now;
            var message = this.messageFactory.CreateMessage(author, conversation, createDate, text);

            message.Conversation = conversation;

            conversation.Messages.Add(message);

            this.messageRepository.Add(message);
        }
    }
}
