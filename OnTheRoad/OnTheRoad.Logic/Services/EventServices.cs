using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Services
{
    public class EventServices : IEventService
    {
        private readonly IEventFactory eventFactory;

        public EventServices(IEventFactory eventFactory)
        {
            this.eventFactory = eventFactory;
        }

        public IEvent GetEvent()
        {
            return this.eventFactory.CreateEvent("Event", "Destination");
        }
    }
}
