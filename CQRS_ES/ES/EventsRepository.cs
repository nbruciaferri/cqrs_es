using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS_ES.ES
{
    public class EventsRepository
    {
        public Dictionary<Guid, List<IEvent>> Events;

        public EventsRepository()
        {
            Events = new Dictionary<Guid, List<IEvent>>();
        }

        public List<Guid> GetUncommittedEvents()
        {
            return Events.Where(x => x.Value.Count > 0).ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
        }
    }
}
