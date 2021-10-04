using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CQRS_ES.ES
{
    public class EventsStore : IEventStore
    {

        private static readonly Dictionary<Guid, List<IEvent>> _eventsRepository = new Dictionary<Guid, List<IEvent>>();


        public EventsStore()
        {

        }

        public int GetLastEventNumber(Guid aggregateId)
        {
            return GetEventsByAggregate(aggregateId).LastOrDefault() == null ? -1 : ((Event)GetEventsByAggregate(aggregateId).LastOrDefault()).EventNumber;
        }

        public List<Guid> GetSavedAggregateIds()
        {
            return _eventsRepository.Keys.Distinct().ToList();
        }

        public List<IEvent> GetEventsByAggregate(Guid aggregateId)
        {
            if (!_eventsRepository.ContainsKey(aggregateId))
                return new List<IEvent>();

            return _eventsRepository[aggregateId];
        }

        public void SaveEvents(Guid aggregateId, Dictionary<Guid, List<IEvent>> events, int eventNumber)
        {
            if (!_eventsRepository.ContainsKey(aggregateId))
            {
                _eventsRepository.Add(aggregateId, new List<IEvent>());
            }

            else if (((Event)_eventsRepository[aggregateId].LastOrDefault()).EventNumber != eventNumber && eventNumber != -1)
                throw new Exception();

            int en = eventNumber;
            if (events.ContainsKey(aggregateId))
            {
                foreach (var @event in events[aggregateId])
                {
                    en++;
                    @event.SetEventNumber(en);

                    _eventsRepository[aggregateId].Add(@event);
                }


                events[aggregateId].Clear();

                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(@"Events_DB.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, _eventsRepository);
                }
            }
        }
    }
}
