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

        /// <summary>
        /// Gets Events that has not been already saved to the EventsStore
        /// </summary>
        /// <returns> A list of aggregate ids of the uncomitted events </returns>
        public List<Guid> GetUncommittedEvents()
        {
            return Events.Where(x => x.Value.Count > 0).ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
        }
    }
}
