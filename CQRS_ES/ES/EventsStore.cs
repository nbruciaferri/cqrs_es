using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS_ES.ES
{
    public class EventsStore : IEventStore
    {

        private static readonly Dictionary<Guid, List<IEvent>> _eventsRepository = new Dictionary<Guid, List<IEvent>>();


        public EventsStore()
        {
        }

        public List<IEvent> GetEventsByAggregate(Guid aggregateId)
        {
            if (!_eventsRepository.ContainsKey(aggregateId))
                throw new Exception();

            return _eventsRepository[aggregateId];
        }

        public void SaveEvents(Guid aggregateId, List<IEvent> events, int eventNumber)
        {
            if (!_eventsRepository.ContainsKey(aggregateId))
            {
                _eventsRepository.Add(aggregateId, new List<IEvent>());
            }

            else if (((Event)_eventsRepository[aggregateId].LastOrDefault()).EventNumber != eventNumber && eventNumber != -1)
                throw new Exception();

            int en = eventNumber;

            foreach(var @event in events)
            {
                en++;
                @event.SetEventNumber(en);

                _eventsRepository[aggregateId].Add(@event);
            }

            events.Clear();
        }
    }
}
