using System;
using System.Collections.Generic;
using CQRS_ES.ES;

namespace CQRS_ES.CQRS
{
    public class QueryModel
    {
        private readonly EventsStore _eventsStore;
        private readonly Guid _aggregateId;

        public QueryModel(EventsStore eventsStore, Guid aggregateId)
        {
            _eventsStore = eventsStore;
            _aggregateId = aggregateId;
        }

        public void ShowProduct()
        {
            int totalQuantity = GetTotalQuantity();
            foreach (var @event in _eventsStore.GetEventsByAggregate(_aggregateId))
                Console.WriteLine(@event.ToString() + " - TOTAL QUANTITY - " + totalQuantity);
        }

        private int GetTotalQuantity()
        {
            int totalQuantity = 0;
            _eventsStore.GetEventsByAggregate(_aggregateId).ForEach(x => totalQuantity += ((Event)x).Quantity);
            return totalQuantity;
        }
    }
}
