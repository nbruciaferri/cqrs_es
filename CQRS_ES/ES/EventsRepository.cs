using System;
using System.Collections.Generic;

namespace CQRS_ES.ES
{
    public class EventsRepository
    {
        public List<IEvent> Events;
        public bool UncommittedEvents { get => Events.Count > 0; }

        public EventsRepository()
        {
            Events = new List<IEvent>();
        }
    }
}
