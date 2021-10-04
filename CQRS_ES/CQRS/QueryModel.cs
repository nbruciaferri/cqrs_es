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
        public void ShowProduct()
        {
            int totalQuantity = GetTotalQuantity();
            foreach (var @event in _eventsStore.GetEventsByAggregate(_aggregateId))
                Console.WriteLine(@event.ToString() + " - TOTAL QUANTITY - " + totalQuantity);
        }

        /// <summary>
        /// Gets total quantity for the desired aggregateId
        /// </summary>
        /// <returns> The total quantity of the product </returns>
        private int GetTotalQuantity()
        {
            int totalQuantity = 0;
            _eventsStore.GetEventsByAggregate(_aggregateId).ForEach(x => totalQuantity += ((Event)x).Quantity);
            return totalQuantity;
        }
    }
}
