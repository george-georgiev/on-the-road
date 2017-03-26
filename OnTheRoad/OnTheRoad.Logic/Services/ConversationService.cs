using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationHelper conversationHelper;
        private readonly IConversationRepository conversationRepository;
        private readonly IUnitOfWork unitOfWork;

        public ConversationService(IConversationRepository conversationRepository, IUnitOfWork unitOfWork, IConversationHelper conversationHelper)
        {
            if (conversationRepository == null)
            {
                throw new ArgumentNullException("conversationRepository can not be null!");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork can not be null!");
            }

            if (conversationHelper == null)
            {
                throw new ArgumentNullException("conversationHelper can not be null!");
            }

            this.conversationRepository = conversationRepository;
            this.unitOfWork = unitOfWork;
            this.conversationHelper = conversationHelper;
        }

        public void SendMessage(string text, string fromUsername, string toUsername)
        {
            var conversationExist = this.conversationRepository.DoesConversationForUsersExist(fromUsername, toUsername);
            IConversation conversation;
            if (conversationExist)
            {
                conversation = this.conversationRepository.GetConversationForUsers(fromUsername, toUsername);
            }
            else
            {
                conversation = this.conversationHelper.CreateConversation(fromUsername, toUsername);
                this.conversationRepository.Add(conversation);
                this.unitOfWork.Commit();
                conversation = this.conversationRepository.GetConversationForUsers(fromUsername, toUsername);
            }

            conversationHelper.AddMessage(conversation, text, fromUsername);
            this.unitOfWork.Commit();
        }

        public IConversation GetConversationForUsers(string firstUsername, string secondUsername)
        {
            var conversation = this.conversationRepository.GetConversationForUsers(firstUsername, secondUsername);

            return conversation;
        }

        public bool DoesConversationForUsersExist(string firstUsername, string secondUsername)
        {
            var exist = this.conversationRepository.DoesConversationForUsersExist(firstUsername, secondUsername);

            return exist;
        }

        public IEnumerable<IConversation> GetConversationsForUser(string username)
        {
            var converstations = this.conversationRepository.GetAllUserConversations(username);

            return converstations;
        }
    }
}
