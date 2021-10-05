using System;
using System.Collections.Generic;
using CQRS_ES.ES;

namespace CQRS_ES.CQRS
{
    /// <summary>
    /// Query model to show data to the client
    /// </summary>
    public class QueryModel
    {
        private readonly EventsStore _eventsStore;
        private readonly Guid _aggregateId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventsStore"> The events store to retrieve data </param>
        /// <param name="aggregateId"> The aggregate id used to filter results </param>
        public QueryModel(EventsStore eventsStore, Guid aggregateId)
        {
            _eventsStore = eventsStore;
            _aggregateId = aggregateId;
        }

        /// <summary>
        /// Shows data to the client
        /// </summary>
        public List<string> ShowProduct()
        {
            List<string> retValue = new List<string>();
            foreach (var @event in _eventsStore.GetEventsByAggregate(_aggregateId))
                retValue.Add(@event.ToString());
            
            return retValue;
        }

    }
}
