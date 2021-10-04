using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CQRS_ES.ES
{
    /// <summary>
    /// This class is used to save and get events to/from the db
    /// </summary>
    public class EventsStore : IEventStore
    {

        private static readonly Dictionary<Guid, List<IEvent>> _eventsRepository = new Dictionary<Guid, List<IEvent>>();

        public EventsStore()
        {
        }

        public Dictionary<Guid, List<IEvent>> GetEventsRepository()
        {
            return _eventsRepository;
        }

        /// <summary>
        /// Gets the eventNumber of the last saved event
        /// </summary>
        /// <param name="aggregateId"> The aggregate id to check for last event </param>
        /// <returns> last event number </returns>
        public int GetLastEventNumber(Guid aggregateId)
        {
            return GetEventsByAggregate(aggregateId).LastOrDefault() == null ? -1 : ((Event)GetEventsByAggregate(aggregateId).LastOrDefault()).EventNumber;
        }

        /// <summary>
        /// Gets aggregate ids present in the db
        /// </summary>
        /// <returns> List of saved aggregate ids </returns>
        public List<Guid> GetSavedAggregateIds()
        {
            return _eventsRepository.Keys.Distinct().ToList();
        }
        
        /// <summary>
        /// Gets Events filtered by aggregate id
        /// </summary>
        /// <param name="aggregateId"> aggregate id for which filter </param>
        /// <returns> List of events for the specified aggregate id </returns>
        public List<IEvent> GetEventsByAggregate(Guid aggregateId)
        {
            if (!_eventsRepository.ContainsKey(aggregateId))
                return new List<IEvent>();

            return _eventsRepository[aggregateId];
        }

        /// <summary>
        /// Save all the uncommitted events for the specified aggregate id
        /// </summary>
        /// <param name="aggregateId"> the aggregate id</param>
        /// <param name="events"> List of events to save </param>
        /// <param name="eventNumber"> the last event number saved for that aggregate id </param>
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
