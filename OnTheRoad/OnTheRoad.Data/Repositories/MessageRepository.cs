using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRoad.Data.Contracts;
using AutoMapper;

namespace OnTheRoad.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message, IMessage>, IMessageRepository
    {
        public MessageRepository(IOnTheRoadDbContext context) : base(context)
        {
        }

        protected override void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IMessage, Message>()
                    .ForMember(x => x.Author, opt => opt.Ignore())
                    .ForMember(x => x.Conversation, opt => opt.Ignore());
            }); 
        }
    }
}
