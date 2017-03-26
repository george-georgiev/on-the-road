using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRoad.Data.Contracts;
using AutoMapper;
using System.Data.Entity;

namespace OnTheRoad.Data.Repositories
{
    public class ConversationRepository : BaseRepository<Conversation, IConversation>, IConversationRepository
    {
        public ConversationRepository(IOnTheRoadDbContext context) : base(context)
        {
        }

        public IEnumerable<IConversation> GetAllUserConversations(string username)
        {
            var conversations = this.Context.Conversations
                .Where(c => c.FirstUser.UserName == username || c.SecondUser.UserName == username)
                //.Select(c => new
                //{
                //    Conversation = c,
                //    Messages = c.Messages.OrderBy(m => m.CreateDate).Take(1).ToList()
                //})
                //.Select(c => c.Conversation)
                .Include(x => x.FirstUser)
                .Include(x => x.SecondUser)
                .Include(x => x.Messages)
                .Include(
                    x => x.Messages
                        .Select(s => s.Author)
                );

            var mapped = new List<IConversation>();
            foreach (var conversation in conversations)
            {
                var mappedConversation = this.MapEntityToDomain(conversation);
                mapped.Add(mappedConversation);
            }

            return mapped;
        }

        public IConversation GetConversationForUsers(string firstUsername, string secondUsername)
        {
            var conversation = this.Context.Conversations
               .Where(c => (c.FirstUser.UserName == firstUsername || c.SecondUser.UserName == firstUsername)
                    && (c.FirstUser.UserName == secondUsername || c.SecondUser.UserName == secondUsername))
               .Single();

            var mapped = this.MapEntityToDomain(conversation);

            return mapped;
        }

        public bool DoesConversationForUsersExist(string firstUsername, string secondUsername)
        {
            var exist = this.Context.Conversations
               .Any(c => (c.FirstUser.UserName == firstUsername || c.SecondUser.UserName == firstUsername)
               && (c.FirstUser.UserName == secondUsername || c.SecondUser.UserName == secondUsername));

            return exist;
        }

        protected override void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IConversation, Conversation>()
                    .ForMember(x => x.FirstUser, opt => opt.Ignore())
                    .ForMember(x => x.SecondUser, opt => opt.Ignore());

                config.CreateMap<IMessage, Message>()
                    .ForMember(x => x.Conversation, opt => opt.Ignore())
                    .ForMember(x => x.Author, opt => opt.Ignore());
            });
        }

        protected override IConversation MapEntityToDomain(Conversation entity)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Conversation, IConversation>();

                config.CreateMap<Message, IMessage>()
                    .ForMember(x => x.Conversation, opt => opt.Ignore());

                config.CreateMap<User, IUser>()
                    .ForMember(x => x.City, opt => opt.Ignore())
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore())
                    .ForMember(x => x.Conversations, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore());
            });

            var domain = Mapper.Map<IConversation>(entity);

            return domain;
        }
    }
}
